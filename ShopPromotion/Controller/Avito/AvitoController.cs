using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace ShopPromotion.Controller.Avito
{
    class Controller : WebDriverHelper, IShopController
    {
        protected override IWebDriver CreateWebDriver()
        {
            var options = new ChromeOptions();
            // start new chrome as incognito
            //options.AddArguments("--incognito");

            // auto loading updates for chrome driver
            try
            {
                var chromeDriverDirName = Path.GetDirectoryName(new DriverManager().SetUpDriver(new ChromeConfig(), "MatchingBrowser"));
                return new ChromeDriver(chromeDriverDirName, options);
            }
            catch (Exception)
            {
                return new ChromeDriver(options);
            }
        }

        public void OpenMainShopPage()
        {
            base.OpenNewTab("https://www.avito.ru/");
        }

        /// <exception cref="T:OpenQA.Selenium.NoSuchWindowException">If the window cannot be found.</exception>
        public override void OpenNewTab(string newUrl)
        {
            base.OpenNewTab(newUrl);

            void AvoidFirewall()
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
                try
                {
                    IWebElement firewallBanner = wait.Until(driver => driver.FindElement(By.ClassName("firewall-container")));
                }
                catch (NoSuchElementException)
                {
                    return;
                }

                Thread.Sleep(4000);
                OpenInCurrentWindow(newUrl);
            }
            AvoidFirewall();
        }

        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        public void AddCurrentItemToCard()
        {
            IWebElement element = Driver.FindElement(By.ClassName("add-to-cart-form"));
            ScrollToElement(element);

#if !DEBUG
            Thread.Sleep(1300);
#endif

            element.Click();

            // wait for adding to card
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            //wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            IWebElement addedToCardCheck = wait.Until(driver => driver.FindElement(By.ClassName("proceed-to-checkout")));

            if (!addedToCardCheck.Displayed)
                throw new NoSuchElementException("Не удалось добавить в корзину.");
        }

        /// <exception cref="T:OpenQA.Selenium.WebDriverException">If no element matches the criteria.</exception>
        public void PreviewPhotos()
        {
            var random = new Random();
            void Wait(bool waitVideo = false)
            {
                int minimumWaitingMillisecondSpan = waitVideo ? 9000 : 2000;
#if DEBUG
                minimumWaitingMillisecondSpan /= 6;
#endif

                Thread.Sleep(TimeSpan.FromMilliseconds(random.Next(minimumWaitingMillisecondSpan, minimumWaitingMillisecondSpan + 3000)));
            }

            void PlayVideo()
            {
                try
                {
                    var videoButton = Driver.FindElement(By.XPath("//*[contains(@class, 'videoPlayer-button')]"));
                    videoButton.Click();
                }
                catch
                { }
            }

            if (!GetNavigatorButtons(out IWebElement prevPicture, out IWebElement nextPicture))
            {
                Wait();
                return;
            }

            List<PreviewImages> getPreviewImagesList = GetPreviewImagesList();
            if (getPreviewImagesList.Count < 3)
            {
                nextPicture.Click();
                Wait();
                nextPicture.Click();
                Wait();
            }
            else
            {
                bool videoWasWatched = false;

                var firstElement = getPreviewImagesList.First().ImageElement;

                // прокликиваем кнопки вперёд с двойным ожиданием на видео
                int countPreviewPhotos = Math.Min(getPreviewImagesList.Count, 7);
                while (countPreviewPhotos != 0)
                {
                    var previewElement = getPreviewImagesList.First();
                    if (previewElement.IsVideo && !videoWasWatched)
                    {
                        PlayVideo();
                        videoWasWatched = true;

                        Wait(true);

                        getPreviewImagesList.RemoveAt(0);
                        --countPreviewPhotos;

                        // After we watched a video the button disappear, we switch to another element
                        if (countPreviewPhotos != 0)
                            getPreviewImagesList.First().ImageElement.Click();
                        else
                            firstElement.Click();

                        if (!GetNavigatorButtons(out prevPicture, out nextPicture))
                        {
                            Wait();
                            return;
                        }
                    }
                    else
                    {
                        Wait(false);
                        nextPicture.Click();

                        getPreviewImagesList.RemoveAt(0);
                        --countPreviewPhotos;
                    }
                }

                Wait();
                prevPicture.Click();
                Wait();

                if (videoWasWatched || !getPreviewImagesList.Exists(element => element.IsVideo))
                    return;

                // смотрим видео которое не посмотрели
                PreviewImages videoImage = getPreviewImagesList.Find(image => image.IsVideo);
                videoImage.ImageElement.Click();
                PlayVideo();

                Wait(true);
            }
        }

        /// <exception cref="T:OpenQA.Selenium.WebDriverException">If no element matches the criteria.</exception>
        public void WatchComments(int maxPagesToWatch)
        {
            void ScrollAllCommentsOnPage(ISearchContext commentsGroupElement, ref int startIndex)
            {
                List<IWebElement> comments = commentsGroupElement.FindElements(By.XPath("//*[contains(@class, 'ReviewSnippet-root')]")).ToList();
                if (!comments.Any())
                    return;

                for (int i = startIndex; i < comments.Count; ++i)
                {
                    ScrollToElement(comments[i]);
#if !DEBUG
                    Thread.Sleep(TimeSpan.FromMilliseconds(new Random(DateTime.Now.Millisecond).Next(1500, 2500)));
#endif
                }

                startIndex += comments.Count;
            }

            try
            {
                try
                {
                    IWebElement commentsButton = Driver.FindElement(By.XPath("//*[@data-marker='rating-caption/rating']"));
                    commentsButton.Click();
                    Thread.Sleep(2000);
                }
                catch (NoSuchElementException)
                {
                    return;
                }

                int startCommentIndex = 0;
                for (var i = 0; i < maxPagesToWatch; ++i)
                {
                    ISearchContext commentsGroupElement;
                    try
                    {
                        commentsGroupElement = Driver.FindElement(By.XPath("//*[@data-marker='rating-popup/popup']"));
                    }
                    catch (NoSuchElementException)
                    {
                        break;
                    }

                    ScrollAllCommentsOnPage(commentsGroupElement, ref startCommentIndex);

                    Thread.Sleep(3000);
                }

                IWebElement closeButtonparent = Driver.FindElement(By.XPath("//*[contains(@class, 'styles-module-closeButton-')]"));
                IWebElement closeButton = closeButtonparent.FindElement(By.TagName("path"));
                closeButton.Click();
                Thread.Sleep(2000);
            }
            catch (WebDriverException)
            {
            }
        }

        public List<IWebElement> GetSuggestionsFromCurrentShop()
        {
            ISearchContext shopSuggestionsGroupElement = Driver;

            try
            {
                shopSuggestionsGroupElement = Driver.FindElement(By.ClassName("other-info"));
            }
            catch (NoSuchElementException) { }

            try
            {
                shopSuggestionsGroupElement = shopSuggestionsGroupElement.FindElement(By.ClassName("responsive-listing-grid"));
            }
            catch (NoSuchElementException) { }

            try
            {
                var suggestionsList = shopSuggestionsGroupElement.FindElements(By.ClassName("js-merch-stash-check-listing")).ToList();
                return suggestionsList.ConvertAll(suggestion => suggestion.FindElement(By.ClassName("listing-link")));
            }
            catch (NoSuchElementException)
            {
                return new List<IWebElement>();
            }
        }

        public List<IWebElement> GetShopListingsList()
        {
            ISearchContext shopSuggestionsGroupElement = Driver;

            try
            {
                shopSuggestionsGroupElement = Driver.FindElement(ByAttribute.Name("data-listings-container-wrapper"));
            }
            catch (NoSuchElementException) { }

            try
            {
                shopSuggestionsGroupElement = shopSuggestionsGroupElement.FindElement(By.ClassName("data-featured-products-default-grid"));
            }
            catch (NoSuchElementException) { }

            try
            {
                var suggestionsList = shopSuggestionsGroupElement.FindElements(By.ClassName("js-merch-stash-check-listing")).ToList();
                return suggestionsList.ConvertAll(suggestion => suggestion.FindElement(By.ClassName("listing-link")));
            }
            catch (NoSuchElementException)
            {
                return new List<IWebElement>();
            }
        }

        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        public IWebElement FindSellerLink()
        {
            return Driver.FindElement(By.Id("listing-page-cart")).FindElement(By.ClassName("wt-text-link-no-underline"));
        }

        /// <exception cref="T:OpenQA.Selenium.NotFoundException">If no element matches the criteria.</exception>
        public string GetUserLocation()
        {
            OpenMainShopPage();

            // wait card to appear
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            IWebElement locationElement = wait.Until(driver => driver.FindElement(ByAttribute.Value("data-marker", "location/tooltip")));
            // result like   Мы не смогли определить ваш город\r\nИзменить\r\nОставить так
            return locationElement.Text.Split('\r').First().Trim();
        }

        private bool GetNavigatorButtons(out IWebElement prevPicture, out IWebElement nextPicture)
        {
            prevPicture = nextPicture = null;
            try
            {
                // wait for buttons
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(3));
                //wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

                List<IWebElement> navigatorButtons = wait.Until(driver => Driver.FindElements(By.XPath("//*[contains(@class, 'image-frame-controlButtonArea')]"))).ToList();
                if (navigatorButtons.Count == 2)
                {
                    var dataDirectionFirstButton = navigatorButtons[0].GetAttribute("data-delta");
                    if (dataDirectionFirstButton == "-1")
                    {
                        Trace.Assert(navigatorButtons[1].GetAttribute("data-delta") == "1", "У кнопок не совпали атрибуты data-delta");

                        prevPicture = navigatorButtons[0];
                        nextPicture = navigatorButtons[1];

                        return true;
                    }

                    if (dataDirectionFirstButton == "1")
                    {
                        Trace.Assert(navigatorButtons[1].GetAttribute("data-delta") == "-1", "У кнопок не совпали атрибуты data-delta");

                        prevPicture = navigatorButtons[1];
                        nextPicture = navigatorButtons[0];

                        return true;
                    }

                    Trace.Assert(false, "У кнопок навигации не известные значения у атрибута data-delta");
                }
            }
            catch (WebDriverException)
            {
            }

            return false;
        }

        private struct PreviewImages
        {
            public IWebElement ImageElement;

            private bool? _videoImage;
            public bool IsVideo
            {
                get
                {
                    if (_videoImage.HasValue) return _videoImage.Value;

                    try
                    {
                        _videoImage = ImageElement.GetAttribute("data-type") == "video";
                    }
                    catch (StaleElementReferenceException)
                    {
                        _videoImage = false;
                    }

                    return _videoImage.Value;
                }
            }
        }

        private List<PreviewImages> GetPreviewImagesList()
        {
            try
            {
                ISearchContext previewContext = Driver.FindElement(By.XPath("//*[contains(@class, 'images-preview-previewWrapper')]"));

                ReadOnlyCollection<IWebElement> previewItems = previewContext.FindElements(By.XPath(".//li[contains(@class, 'images-preview-previewImageWrapper')]"));

                return previewItems.Select(element => new PreviewImages
                {
                    ImageElement = element
                }).ToList();
            }
            catch (NoSuchElementException)
            {
            }

            return new List<PreviewImages>();
        }
    }
}
