﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using EtsyPromotion.Promotion.Interfaces;
using EtsyPromotion.WebDriver;
using OpenQA.Selenium;

namespace EtsyPromotion.Promotion.Implementation
{
    /// <summary>
    /// Base worker class for executing promotion on listings
    /// </summary>
    /// <typeparam name="TListingInfoType">Class with info about listing<see cref="T:EtsyPromotion.Promotion.Interfaces.ListingInfo">ListingInfo</see></typeparam>
    /// <typeparam name="TControllerType">Type of web driver controller be used for promoting</typeparam>
    internal abstract class PromotionWorkerBase<TListingInfoType, TControllerType>
        where TListingInfoType : ListingInfo
        where TControllerType : EtsyController
    {
        public event EventHandler WhenStart;

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
        public event EventHandler<int> WhenFinishListingPromotion;

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

        /// <returns> True if promotion started successfully </returns>
        public bool StartPromotion(BindingList<TListingInfoType> listingsList)
        {
            _countSuccessfullyPromotedListings = 0;
            ClearErrorMessage();

            if (!InitializeAndCheckListings(listingsList))
            {
                WhenFinish?.Invoke(this,
                    string.IsNullOrEmpty(_errorMessage) ? "Не выбрано ниодного товара для продвижения" : _errorMessage);

                return false;
            }

            Debug.Assert(!IsWorking(), "Поток уже работает");

            WhenStart?.Invoke(this, new EventArgs());

            _workerThread = new Thread(PromotionThread);
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
            _workerThread?.Abort();
        }

        /// <summary>
        /// Initialize function to install listings list, list should be copied/transformed to internal list
        /// If function fails we call WhenFinish with _errorMessage ?? "No listings for promotion"
        /// </summary>
        /// <param name="listingsList">Listings for promotion</param>
        /// <returns>true if we can promote any listings from list, false otherwise.</returns>
        protected abstract bool InitializeAndCheckListings(BindingList<TListingInfoType> listingsList);
        /// <summary>
        /// Creation of web driver controller with default settings
        /// </summary>
        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        /// <returns><see cref="T:EtsyPromotion.WebDriver.EtsyController">EtsyController</see></returns>
        protected abstract TControllerType CreateWebDriverController();
        /// <summary>
        /// Main thread function for executing promotion
        /// </summary>
        /// <param name="controller"></param>
        protected abstract void ExecutePromotion(TControllerType controller);

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
            WhenFinishListingPromotion?.Invoke(this, listingIndex);
        }

        protected void OnException(Exception exception)
        {
            WhenException?.Invoke(this, exception);
        }

        private void PromotionThread()
        {
            bool wasInterrupted = false;
            TControllerType controller = null;
            try
            {
                try
                {
                    controller = CreateWebDriverController();
                }
                catch (WebDriverException exception)
                {
                    throw new WebDriverException("Не удалось создать драйвер для управления хромом.", exception);
                }

                ExecutePromotion(controller);
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
                    controller.Driver.Quit();

                WhenFinish?.Invoke(this, wasInterrupted ? null : _errorMessage);
            }
        }

        protected void InspectCurrentListing(TControllerType controller, int listingIndex, bool addToCard)
        {
            bool failed = false;
            try
            {
                controller.PreviewPhotos();
            }
            catch (WebDriverException exception)
            {
                OnErrorDuringPromotion(listingIndex,
                    $"Возникла ошибка при просмотре фотографий товара, обратитесь к администратору.\n\n {exception}");
            }

            try
            {
                controller.WatchComments();
            }
            catch (WebDriverException exception)
            {
                OnErrorDuringPromotion(listingIndex,
                    $"Возникла ошибка при просмотре комментариев к товару, обратитесь к администратору.\n\n {exception}");
            }

            if (addToCard)
            {
                try
                {
                    controller.AddCurrentItemToCard();
                    Thread.Sleep(1500);
                }
                catch (WebDriverException exception)
                {
                    failed = true;
                    OnErrorDuringPromotion(listingIndex,
                        $"Возникла ошибка при добавлении товара в корзину, возможно у товара есть несколько вариантов добавления в корзину, обратитесь к администратору.\n\n {exception}");
                }
            }

            if (!failed)
                OnSuccessfullyPromotedListing(listingIndex);
        }
    }
}