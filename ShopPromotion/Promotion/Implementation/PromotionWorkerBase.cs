using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using ShopPromotion.Promotion.Interfaces;
using ShopPromotion.Controller;
using OpenQA.Selenium;

namespace ShopPromotion.Promotion.Implementation
{
    /// <summary>
    /// Base worker class for executing promotion on listings
    /// </summary>
    /// <typeparam name="TListingInfoType">Class with info about listing<see cref="T:ShopPromotion.Promotion.Interfaces.ListingInfo">ListingInfo</see></typeparam>
    internal abstract class PromotionWorkerBase<TListingInfoType>
        where TListingInfoType : ListingInfo
    {
        public event EventHandler WhenStart;

        /// <summary> Notification about status changes </summary>
        public event EventHandler<string> WhenStatusUpdated;

        /// <summary> Finishing promotion </summary>
        /// <param> string is null if no error otherwise contain error message</param>
        public event EventHandler<string> WhenFinish;

        /// <summary> Handle exception during promotion </summary>
        public event EventHandler<Exception> WhenException;

        /// <summary> Delegate that some listings was promoted, calling before closing web driver after promotion </summary>
        /// <param>Count of promoted listings</param>
        /// <returns>Return true if need to close web driver, false otherwise </returns>
        public event Func<int, bool> OnSuccessfullyPromoted;

        /// <summary> Finishing promotion on listing by index </summary>
        public event EventHandler<PromotionDone> WhenFinishListingPromotion;

        /// <summary> An error occurred during th e listing promotion </summary>
        public event EventHandler<ErrorDuringListingPromotion> WhenErrorDuringListingPromotion;

        /// <summary> Promotion thread </summary>
        private Thread _workerThread;
        /// <summary>
        /// All error messages which during promotion, on finish we sanding this message via <see cref="T:PromotionWorkerBase.WhenFinish">WhenFinish</see>
        /// <see cref="T:PromotionWorkerBase.OnErrorDuringPromotion">OnErrorDuringPromotion</see>
        /// <see cref="T:PromotionWorkerBase.ClearErrorMessage">ClearErrorMessage</see>
        /// </summary>
        private string _errorMessage;
        private int _countSuccessfullyPromotedListings = 0;
        // used to track current item index for status message
        protected int _promotingItemNumber = 1;

        /// <returns> True if promotion started successfully </returns>
        public bool StartPromotion(List<TListingInfoType> listingsList, RunMode runMode, SiteMode siteMode)
        {
            _countSuccessfullyPromotedListings = 0;
            _promotingItemNumber = 0;
            ClearErrorMessage();

            if (!InitializeAndCheckListings(listingsList, siteMode))
            {
                WhenFinish?.Invoke(this,
                    string.IsNullOrEmpty(_errorMessage) ? "Не выбрано ниодного товара для продвижения, возможно вы не правильно задали ссылки на товары" : _errorMessage);

                return false;
            }

            Debug.Assert(!IsWorking(), "Поток уже работает");

            WhenStart?.Invoke(this, new EventArgs());
            
            UpdateStatus("Starting promotion");

            _workerThread = new Thread(() => PromotionThread(runMode, siteMode));
            _workerThread.Start();

            return true;
        }

        /// <summary> Interrupting promotion </summary>
        public void Interrupt()
        {
            if (IsWorking())
                _workerThread.Interrupt();
        }

        /// <returns>True if promotion executing</returns>
        public bool IsWorking()
        {
            return _workerThread?.IsAlive == true;
        }

        public void Dispose()
        {
            try
            {
                _workerThread?.Abort();
            }
            catch { }
        }

        /// <summary>
        /// Initialize function to install listings list, list should be copied/transformed to internal list
        /// If function fails we call WhenFinish with _errorMessage ?? "No listings for promotion"
        /// </summary>
        /// <param name="listingsList">Listings for promotion</param>
        /// <param name="siteMode">Site mode</param>
        /// <returns>true if we can promote any listings from list, false otherwise.</returns>
        protected abstract bool InitializeAndCheckListings(List<TListingInfoType> listingsList, SiteMode siteMode);
        /// <summary>
        /// Creation of web driver controller with default settings
        /// </summary>
        /// <param name="siteMode">Site mode</param>
        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        /// <returns><see cref="T:ShopPromotion.Controller">EtsyController or AvitoController</see></returns>
        protected abstract IShopController CreateShopController(SiteMode siteMode);
        /// <summary>
        /// Main thread function for executing promotion
        /// </summary>
        /// <param name="controller"></param>
        protected abstract void ExecutePromotion(IShopController controller);

        protected void OnErrorDuringPromotion(int listingIndex, string errorMessage)
        {
            _errorMessage += $"Ошибка при продвижении товара с номером {listingIndex + 1}. {errorMessage}\n\n\n";

            WhenErrorDuringListingPromotion?.Invoke(this, new ErrorDuringListingPromotion
            {
                ElementIndex = listingIndex,
                ErrorMessage = errorMessage
            });
        }

        protected void ClearErrorMessage()
        {
            _errorMessage = null;
        }

        protected void OnSuccessfullyPromotedListing(int listingIndex)
        {
            ++_countSuccessfullyPromotedListings;
            WhenFinishListingPromotion?.Invoke(this, new PromotionDone(listingIndex));
        }

        protected void OnException(Exception exception)
        {
            WhenException?.Invoke(this, exception);
        }

        private void PromotionThread(RunMode runMode, SiteMode siteMode)
        {
            bool wasInterrupted = false;
            IShopController controller = null;
            try
            {
                do
                {
                    _errorMessage = null;

                    try
                    {
                        UpdateStatus("Создаем контроллер");
                        controller = CreateController(siteMode);
                    }
                    catch (WebDriverException exception)
                    {
                        OnException(exception);
                        continue;
                    }
                    
                    try
                    {
                        UpdateStatus("Выполняем промоушен");
                        ExecutePromotion(controller);
                    }
                    finally
                    {
                        controller.Quit();
                    }
                } while (Wait(runMode));
            }
            catch (Exception exception)
            {
                switch (exception)
                {
                    case ThreadInterruptedException _:
                    case ThreadAbortException _:
                        wasInterrupted = true;
                        return;
                }

                OnException(exception);
            }
            finally
            {
                if (controller != null && (wasInterrupted || OnSuccessfullyPromoted?.Invoke(_countSuccessfullyPromotedListings) != false))
                    controller.Quit();

                WhenFinish?.Invoke(this, wasInterrupted ? null : _errorMessage);
            }
        }

        private IShopController CreateController(SiteMode siteMode)
        {
            int attempt = 0, maxAttempts = 5;

            while (true)
            {
                try
                {
                    return CreateShopController(siteMode);
                }
                catch (Exception exception)
                {
                    if (++attempt == maxAttempts)
                    {
                        throw new WebDriverException("Failed to create a shop controller.", exception);
                    }
                }
            }

            throw new InvalidOperationException("Unreachable code");
        }

        private bool Wait(RunMode runMode)
        {
            UpdateStatus("Ждем следующего запуска");
            switch (runMode)
            {
                case RunMode.eOnes:
                    return false;
                case RunMode.eMinutes_5:
                    Thread.Sleep(new TimeSpan(0, 5, 0));
                    break;
                case RunMode.eMinutes_15:
                    Thread.Sleep(new TimeSpan(0, 15, 0));
                    break;
                case RunMode.eHour_1:
                    Thread.Sleep(new TimeSpan(1, 0, 0));
                    break;
                case RunMode.eHour_5:
                    Thread.Sleep(new TimeSpan(5, 0, 0));
                    break;
                case RunMode.eDay:
                    Thread.Sleep(new TimeSpan(1, 0, 0, 0));
                    break;
                case RunMode.eUntilInterrupt:
                    Thread.Sleep(3000);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return true;
        }

        protected void InspectCurrentListing(IShopController controller, int listingIndex, bool addToCard, bool checkComments)
        {
            try
            {
                UpdateStatus("Просматриваем фото");
                controller.PreviewPhotos();
            }
            catch (WebDriverException exception)
            {
                OnErrorDuringPromotion(listingIndex,
                    $"Возникла ошибка при просмотре фотографий товара, обратитесь к администратору.\n\n {exception}");
            }

            if (checkComments)
            {
                try
                {
                    UpdateStatus("Просматриваем комментарии");
                    controller.WatchComments(2);
                }
                catch (WebDriverException exception)
                {
                    OnErrorDuringPromotion(listingIndex,
                        $"Возникла ошибка при просмотре комментариев к товару, обратитесь к администратору.\n\n {exception}");
                }
            }

            if (addToCard)
            {
                try
                {
                    UpdateStatus("Добавляем в корзину/избранное");
                    controller.AddCurrentItemToCart();
                }
                catch (WebDriverException exception)
                {
                    OnErrorDuringPromotion(listingIndex,
                        $"Возникла ошибка при добавлении товара в корзину, возможно товара нет в корзине или у него есть несколько вариантов добавления в корзину, обратитесь к администратору.\n\n {exception}");
                }
            }

            // imitation normal user, preview suggestions from seller or go to seller magazine and take a look on some listings
            bool checkSuggestions = new Random().Next(2) == 0;
            if (checkSuggestions && !PreviewSuggestions(controller))
                PreviewShopListings(controller);
            else if (!checkSuggestions && !PreviewShopListings(controller))
                PreviewSuggestions(controller);

            OnSuccessfullyPromotedListing(listingIndex);
        }

        // Open few random listings from seller suggestions with preview photo and comments
        protected bool PreviewSuggestions(IShopController controller)
        {
            UpdateStatus("Смотрим предлоежения");
            var suggestionsList = controller.GetSuggestionsFromCurrentShop();
            if (!suggestionsList.Any())
                return false;

            Random rnd = new Random();
            for (int i = rnd.Next(3), count = suggestionsList.Count; i < count; i += rnd.Next(1, 4))
            {
                controller.ScrollToElement(suggestionsList[i]);
#if !DEBUG
                Thread.Sleep(1000);
#endif

                UpdateStatus("Открываем предложения");
                controller.OpenInNewTab(suggestionsList[i]);

                try
                {
                    UpdateStatus("Просматриваем фото внутри предложений");
                    controller.PreviewPhotos();
                }
                catch (WebDriverException)
                {}

                try
                {
                    UpdateStatus("Просматриваем комментарии внутри предложений");
                    controller.WatchComments(1);
                }
                catch (WebDriverException)
                {}

                controller.CloseCurrentTab();
            }
            return true;
        }

        // Open seller page in new tab, scroll through listings and open random listings(max 5) with preview photo and comments
        protected bool PreviewShopListings(IShopController controller)
        {
            try
            {
                UpdateStatus("Открываем страницу магазина");
                controller.OpenInNewTab(controller.FindSellerLink());
            }
            catch (WebDriverException)
            {
                return false;
            }

            try
            {
                var shopListings = controller.GetShopListingsList(false);
                if (!shopListings.Any())
                    return false;

                int maxListingsCountToOpen = 5;

                Random rnd = new Random();
                for (int i = rnd.Next(shopListings.Count), count = shopListings.Count;
                    i < count && maxListingsCountToOpen != 0; i = rnd.Next(i + 1, count), --maxListingsCountToOpen)
                {
                    controller.ScrollToElement(shopListings[i]);
#if !DEBUG
                    Thread.Sleep(1000);
#endif
                    UpdateStatus("Открываем листинг мазагина");
                    controller.OpenInNewTab(shopListings[i]);

                    try
                    {
                        UpdateStatus("Просматриваем фото внутри магазина");
                        controller.PreviewPhotos();
                    }
                    catch (WebDriverException)
                    { }

                    try
                    {
                        UpdateStatus("Просматриваем комментарии внутри магазина");
                        controller.WatchComments(1);
                    }
                    catch (WebDriverException)
                    { }

                    controller.CloseCurrentTab();
                }
            }
            catch (WebDriverException)
            {}
            finally
            {
                controller.CloseCurrentTab();
            }
            return true;
        }


        protected void UpdateStatus(string status)
        {
            WhenStatusUpdated?.Invoke(this, $"{_promotingItemNumber}: {status}");
        }
    }
}
