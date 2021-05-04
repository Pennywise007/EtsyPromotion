using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EtsyPromotion.KeywordPromotion
{
    class PromotionExecutor
    {
        public PromotionExecutor(BindingList<ProductsListItem> productsList)
        {
            // validate and transform all parameters
            foreach (var productInfo in productsList)
            {
                if (productInfo.ItemAction == ListingActionDetails.ListingAction.Skip)
                    continue;

                bool failed = false;
                var keyWordsArray = productInfo.KeyWords.Split(';');
                if (!keyWordsArray.Any())
                {
                    failed = true;
                    AddErrorMessage($"Не удалось получить список ключевых слов из '{productInfo.KeyWords}', проверьте формат у элемента '{productInfo.KeyWords}'.");
                }

                string listingID = null;
                try
                {
                    // link example https://www.etsy.com/listing/ID/... try extract ID
                    var match = Regex.Match(productInfo.Link, "etsy.com/listing/(.*)/");
                    listingID = match.Groups[1].Value;
                }
                catch (Exception exception)
                {
                    failed = true;
                    AddErrorMessage(
                        $"Не удалось получить идентификатор из ссылки '{productInfo.Link}', проверьте что ссылка у элемента '{productInfo.KeyWords} валидна\n\n" +
                        exception.ToString());
                }

                if (!failed)
                    m_promotionInfo.Add(new PromotionInfo
                    {
                        m_addToCard = productInfo.ItemAction == ListingActionDetails.ListingAction.AddToCard,
                        m_listingId = listingID,
                        m_keyWords = keyWordsArray.ToList()
                    });
            }

            if (!m_promotionInfo.Any() && string.IsNullOrEmpty(m_errorMessage))
            {
                AddErrorMessage("Не выбрано ниодного товара для продвижения");
            }
        }

        ~PromotionExecutor()
        {
            m_controller?.m_driver.Quit();

            if (!string.IsNullOrEmpty(m_errorMessage))
                MessageBox.Show(m_errorMessage, "При выполнении продвижения возникли ошибки.");
        }

        public void Execute()
        {
            if (!m_promotionInfo.Any())
                return;

            m_controller = new SearchController();
            m_controller.m_driver.Manage().Window.Maximize();

            m_controller.OpenNewTab("https://www.etsy.com/");

            foreach (var productInfo in m_promotionInfo)
            {
                foreach (var keyWord in productInfo.m_keyWords)
                {
                    m_controller.SearchText(keyWord);

                    int currentPage = 0;
                    IWebElement productLink = null;

                    do
                    {
                        if (currentPage != 0)
                        {
                            // whaiting for loading page
                            Thread.Sleep(3000);
                        }

                        ++currentPage;

                        List<IWebElement> allResults = new List<IWebElement>();
                        try
                        {
                            allResults = m_controller.getListOfSearchResults();
                        }
                        catch (NoSuchElementException) {}

                        try
                        {
                            productLink = m_controller.FindListingInSearchResults(productInfo.m_listingId);
                        }
                        catch (NoSuchElementException) {}

                        ScrollSearchResults(allResults, productLink);
                    } while (productLink == null && currentPage < 100 && m_controller.OpenNextSearchPage());

                    if (productLink != null)
                    {
                        try
                        {
                            productLink.Click();

                            m_controller.PreviewPhotos();

                            m_controller.WatchComments();

                            if (productInfo.m_addToCard)
                                m_controller.AddCurrentItemToCard();
                        }
                        catch (Exception)
                        {
                            AddErrorMessage(
                                $"Не удалось добавить товар с идентификатором {productInfo.m_listingId} в корзину.");
                        }
                    }
                    else
                    {
                        if (currentPage < 2)
                        {
                            AddErrorMessage(
                                $"Не удалось найти товар c {productInfo.m_listingId} по ключевому слову {keyWord}.\n" +
                                "Проверьте что по ключевым словам есть результаты поиска. " +
                                "Если есть много страниц с результатами значит программа не смогла переключить страницы, обратитесь к автору.");
                        }
                        else
                        {
                            AddErrorMessage(
                                $"Не удалось найти товар c идентификатором {productInfo.m_listingId} по ключевому слову {keyWord}.");
                        }
                    }
                }
            }
        }

        public void AddErrorMessage(string message)
        {
            m_errorMessage += message + "\n";
        }

        public void ClearErrorMessage()
        {
            m_errorMessage = null;
        }

        private void ScrollSearchResults(List<IWebElement> allResults, IWebElement productLink)
        {
            int? stopIndex = null;

            if (productLink != null)
            {
                try
                {
                    stopIndex = allResults.FindIndex(element => element.TagName == productLink.TagName);
                }
                catch (ArgumentNullException)
                {
                    Debug.Assert(false);
                }
            }

            for (int currentIndex = 0, maxIndex = stopIndex ?? allResults.Count;
                currentIndex < maxIndex;
                currentIndex += 7)
            {
                if (m_controller.ScrollToElement(allResults[currentIndex]))
                    Thread.Sleep(1000);
            }

            if (stopIndex == null)
                return;

            if (m_controller.ScrollToElement(allResults[stopIndex.Value]))
                Thread.Sleep(1000);
        }

        private class PromotionInfo
        {
            public bool m_addToCard;
            public string m_listingId;
            public List<string> m_keyWords;
        }

        private SearchController m_controller;
        private List<PromotionInfo> m_promotionInfo = new List<PromotionInfo>();
        private string m_errorMessage;
    }
}
