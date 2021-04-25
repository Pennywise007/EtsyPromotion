using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EtsyPromotion
{
    class EtsyController : ChromeDriverHelper
    {
        public EtsyController(IWebDriver driver) : base(driver)
        { }

        public void AddCurrentItemToCard()
        {
            IWebElement element = m_driver.FindElement(By.ClassName("add-to-cart-form"));
            ScrollToElement(element);

            Thread.Sleep(1300);

            element.Click();

            // wait for adding to card
            var wait = new WebDriverWait(m_driver, TimeSpan.FromSeconds(10));
            //wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            IWebElement addedToCardCheck = wait.Until(driver => driver.FindElement(By.ClassName("proceed-to-checkout")));

            if (!addedToCardCheck.Displayed)
                throw new Exception("Не удалось добавить в корзину.");
        }

        public void PreviewPhotos()
        {
            var random = new Random();
            void Wait(bool waitVideo = false)
            {
                int minimumWaitingMillisecondSpan = waitVideo ? 9000 : 2000;
                Thread.Sleep(TimeSpan.FromMilliseconds(random.Next(minimumWaitingMillisecondSpan, minimumWaitingMillisecondSpan + 3000)));
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

                // прокликиваем кнопки вперёд с двойным ожиданием на видео
                int countPreviewPhotos = Math.Min(getPreviewImagesList.Count, 7);
                while (countPreviewPhotos != 0)
                {
                    var previewElement = getPreviewImagesList.First();
                    videoWasWatched |= previewElement.IsVideo;

                    Wait(previewElement.IsVideo);
                    nextPicture.Click();

                    getPreviewImagesList.RemoveAt(0);
                    --countPreviewPhotos;
                }

                Wait();
                prevPicture.Click();
                Wait();

                if (videoWasWatched || !getPreviewImagesList.Exists(element => element.IsVideo))
                    return;

                // смотрим видео которое не посмотрели
                PreviewImages videoImage = getPreviewImagesList.Find(image => image.IsVideo);
                videoImage.m_imageElement.Click();

                Wait(true);
            }
        }

        public void WatchComments()
        {
            ISearchContext commentsGroupElement;

            try
            {
                commentsGroupElement = m_driver.FindElement(AttributeHelper.SelectorByAttributeValue("data-appears-component-name", "listing_page_reviews"));
            }
            catch (NoSuchElementException)
            {
                return;
            }

            void ScrollAllCommentsOnPage()
            {
                List<IWebElement> comments = commentsGroupElement.FindElements(By.ClassName("wt-grid__item-xs-12")).ToList();
                if (!comments.Any())
                    return;

                foreach (var commentElement in comments)
                {
                    ScrollToElement(commentElement);
                    Thread.Sleep(1000);
                }
            }

            bool GoToNextPage()
            {
                List<IWebElement> commentPagesAndForwardButtons = commentsGroupElement.FindElements(By.ClassName("wt-action-group__item-container")).ToList();
                if (!commentPagesAndForwardButtons.Any())
                    return false;

                var nextCommentsPageButton = commentPagesAndForwardButtons.Last();

                ScrollToElement(nextCommentsPageButton);
                nextCommentsPageButton.Click();
                return true;
            }

            try
            {
                for (var i = 0; i < 2; ++i)
                {
                    ScrollAllCommentsOnPage();
                    if (!GoToNextPage())
                        break;

                    Thread.Sleep(3000);
                }
            }
            catch (Exception)
            {
            }
        }

        public List<IWebElement> GetSuggestionsFromThisShop()
        {
            ISearchContext shopSuggestionsGroupElement = m_driver;

            try
            {
                shopSuggestionsGroupElement = m_driver.FindElement(By.ClassName("other-info"));
            }
            catch (NoSuchElementException) {}

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


        private List<IWebElement> findCollection(ISearchContext context, string name)
        {
            IReadOnlyCollection<IWebElement> classSelector = context.FindElements(By.ClassName(name));
            IReadOnlyCollection<IWebElement> cssSelector = context.FindElements(By.CssSelector(name));
            IReadOnlyCollection<IWebElement> attributeCSSSelector = context.FindElements(AttributeHelper.SelectorByAttribute(name));
            IReadOnlyCollection<IWebElement> IdSelector = context.FindElements(By.Id(name));
            IReadOnlyCollection<IWebElement> nameSelector = context.FindElements(By.Name(name));
            IReadOnlyCollection<IWebElement> tagSelector = context.FindElements(By.TagName(name));
            IReadOnlyCollection<IWebElement> xPathSelector = context.FindElements(By.XPath(name));
            IReadOnlyCollection<IWebElement> linkSelector = context.FindElements(By.LinkText(name));
            IReadOnlyCollection<IWebElement> particularLinkSelector = context.FindElements(By.PartialLinkText(name));

            int countNotEmpty = (classSelector.Count() != 0 ? 1 : 0) +
                                (cssSelector.Count() != 0 ? 1 : 0) +
                                (attributeCSSSelector.Count() != 0 ? 1 : 0) +
                                (IdSelector.Count() != 0 ? 1 : 0) +
                                (nameSelector.Count() != 0 ? 1 : 0) +
                                (tagSelector.Count() != 0 ? 1 : 0) +
                                (xPathSelector.Count() != 0 ? 1 : 0) +
                                (linkSelector.Count() != 0 ? 1 : 0) +
                                (particularLinkSelector.Count() != 0 ? 1 : 0);

            Trace.Assert(countNotEmpty <= 1);

            if (classSelector.Count != 0)
                return classSelector.ToList();

            if (cssSelector.Count != 0)
                return cssSelector.ToList();

            if (attributeCSSSelector.Count != 0)
                return attributeCSSSelector.ToList();

            if (IdSelector.Count != 0)
                return IdSelector.ToList();

            if (nameSelector.Count != 0)
                return nameSelector.ToList();

            if (tagSelector.Count != 0)
                return tagSelector.ToList();

            if (xPathSelector.Count != 0)
                return xPathSelector.ToList();

            if (linkSelector.Count != 0)
                return linkSelector.ToList();

            if (particularLinkSelector.Count != 0)
                return particularLinkSelector.ToList();

            return new List<IWebElement>();
        }

        public string GetEtsyUserLocation()
        {
            try
            {
                OpenNewTab("https://www.etsy.com/");

                IWebElement locationElement = m_driver.FindElement(By.ClassName("footer-redesign")).FindElements(By.ClassName("wt-display-inline-block"))[1];
                // result like   Россия   |   Русский   |   $ (USD), take first
                return locationElement.Text.Split('|').First().Trim();
            }
            finally
            {
                CloseCurrentTab();
            }
        }

        private bool GetNavigatorButtons(out IWebElement prevPicture, out IWebElement nextPicture)
        {
            prevPicture = nextPicture = null;
            try
            {
                ISearchContext buttonsContext = m_driver.FindElement(By.ClassName("listing-page-image-carousel-component")) ?? (ISearchContext)m_driver;

                List<IWebElement> navigatorButtons = buttonsContext.FindElements(AttributeHelper.SelectorByAttribute("data-carousel-nav-button", "button")).ToList();

                if (navigatorButtons.Count == 2)
                {
                    var dataDirectionFirstButton = navigatorButtons[0].GetAttribute("data-direction");
                    if (dataDirectionFirstButton == "prev")
                    {
                        Trace.Assert(navigatorButtons[1].GetAttribute("data-direction") == "next", "У кнопок не совпали атрибуты data-direction");

                        prevPicture = navigatorButtons[0];
                        nextPicture = navigatorButtons[1];

                        return true;
                    }

                    if (dataDirectionFirstButton == "next")
                    {
                        Trace.Assert(navigatorButtons[1].GetAttribute("data-direction") == "prev", "У кнопок не совпали атрибуты data-direction");

                        prevPicture = navigatorButtons[1];
                        nextPicture = navigatorButtons[0];

                        return true;
                    }

                    Trace.Assert(false, "У кнопок навигации не известные значения у атрибута data-direction");
                }
            }
            catch (Exception)
            {
            }

            return false;
        }

        private struct PreviewImages
        {
            public IWebElement m_imageElement;

            private bool? _videoImage;
            public bool IsVideo
            {
                get
                {
                    if (_videoImage.HasValue) return _videoImage.Value;

                    try
                    {
                        _videoImage = AttributeHelper.IsAttributeExist(m_imageElement, "data-carousel-thumbnail-video");
                    }
                    catch (Exception)
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
                ISearchContext buttonsContext = m_driver.FindElement(By.ClassName("listing-page-image-carousel-component")) ?? (ISearchContext)m_driver;

                ReadOnlyCollection<IWebElement> images = buttonsContext.FindElements(AttributeHelper.SelectorByAttribute("data-carousel-pagination-item", "li"));

                return images.Select(element => new PreviewImages
                {
                    m_imageElement = element
                }).ToList();
            }
            catch (Exception)
            {
            }

            return new List<PreviewImages>();
        }
    }
}
