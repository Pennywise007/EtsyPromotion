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
    class PromotionExecutor : IDisposable
    {
        public PromotionExecutor(BindingList<ProductsListItem> productsList, Action<int> onEndExecutionForElementByIndex)
        {
            m_onEndExecutionForElementByIndex = onEndExecutionForElementByIndex;

            // validate and transform all parameters
            for (int index = 0; index < productsList.Count; ++index)
            {
                var productInfo = productsList[index];
                if (productInfo.ItemAction == ListingActionDetails.ListingAction.Skip ||
                    string.IsNullOrEmpty(productInfo.Link))
                    continue;

                bool failed = false;
                var keyWordsArray = productInfo.KeyWords.Split(';');
                if (!keyWordsArray.Any())
                {
                    failed = true;
                    AddErrorMessage($"Не удалось получить список ключевых слов из '{productInfo.KeyWords}'(номер в таблице {index + 1}), проверьте формат у элемента '{productInfo.KeyWords}'.");
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
                        $"Не удалось получить идентификатор из ссылки '{productInfo.Link}'(номер в таблице {index + 1}), проверьте что ссылка у элемента '{productInfo.KeyWords} валидна и её формат https://www.etsy.com/listing/123123123/...\n\n" +
                        exception.ToString());
                }

                if (!failed)
                    m_promotionInfo.Add(new PromotionInfo
                    {
                        m_addToCard = productInfo.ItemAction == ListingActionDetails.ListingAction.AddToCard,
                        m_elementIndexInProductsList = index,
                        m_listingId = listingID,
                        m_keyWords = keyWordsArray.ToList()
                    });
            }

            if (!m_promotionInfo.Any() && string.IsNullOrEmpty(m_errorMessage))
            {
                AddErrorMessage("Не выбрано ниодного товара для продвижения");
            }
        }

        public void Dispose()
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
                            allResults = m_controller.GetListOfSearchResults();
                        }
                        catch (NoSuchElementException) {}

                        try
                        {
                            productLink = m_controller.FindListingInSearchResults(productInfo.m_listingId);
                        }
                        catch (NoSuchElementException) {}

                        ScrollSearchResults(allResults, productLink);
                    } while (productLink == null && currentPage < 100 && m_controller.OpenNextSearchPage(currentPage + 1));

                    if (productLink != null)
                    {
                        try
                        {
                            productLink.Click();

                            m_controller.PreviewPhotos();

                            m_controller.WatchComments();

                            if (productInfo.m_addToCard)
                                m_controller.AddCurrentItemToCard();

                            m_onEndExecutionForElementByIndex(productInfo.m_elementIndexInProductsList);
                        }
                        catch (Exception)
                        {
                            AddErrorMessage(
                                $"Не удалось добавить товар с идентификатором {productInfo.m_listingId}(номер в таблице {productInfo.m_elementIndexInProductsList + 1}) в корзину.");
                        }
                    }
                    else
                    {
                        if (currentPage < 2)
                        {
                            AddErrorMessage(
                                $"Не удалось найти товар c идентификатором {productInfo.m_listingId}(номер в таблице {productInfo.m_elementIndexInProductsList + 1}) по ключевому слову {keyWord}.\n" +
                                "Проверьте что по ключевым словам есть результаты поиска. " +
                                "Если есть много страниц с результатами значит программа не смогла переключить страницы, обратитесь к автору.");
                        }
                        else
                        {
                            AddErrorMessage(
                                $"Не удалось найти товар c идентификатором {productInfo.m_listingId}(номер в таблице {productInfo.m_elementIndexInProductsList + 1}) по ключевому слову {keyWord}.");
                        }
                    }
                }
            }
        }

        public void AddErrorMessage(string message)
        {
            m_errorMessage += message + "\n\n\n";
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

#if DEBUG
            const int waitingTimeMillisec = 10;
#else
            const int waitingTimeMillisec = 1000;
#endif

            for (int currentIndex = 0, maxIndex = stopIndex ?? allResults.Count;
                currentIndex < maxIndex;
                currentIndex += 7)
            {
                if (m_controller.ScrollToElement(allResults[currentIndex]))
                    Thread.Sleep(waitingTimeMillisec);
            }

            if (stopIndex == null)
                return;

            if (m_controller.ScrollToElement(allResults[stopIndex.Value]))
                Thread.Sleep(waitingTimeMillisec);
        }

        private class PromotionInfo
        {
            public bool m_addToCard;
            public int m_elementIndexInProductsList;
            public string m_listingId;
            public List<string> m_keyWords;
        }

        private SearchController m_controller;
        private List<PromotionInfo> m_promotionInfo = new List<PromotionInfo>();
        private Action<int> m_onEndExecutionForElementByIndex;
        private string m_errorMessage;
    }
}
