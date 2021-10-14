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
using EtsyPromotion.Promotion.Interfaces;
using EtsyPromotion.WebDriver;
using OpenQA.Selenium;

namespace EtsyPromotion.Promotion.Implementation
{
    internal class KeyWordPromotionWorker : PromotionWorkerBase<KeyWordsListingInfo, SearchController>, IKeyWordPromotionWorker
    {
        /// <summary>
        /// Transformed listings info into promotion list
        /// </summary>
        private readonly List<PromotionInfo> _promotionList = new List<PromotionInfo>();

        protected override bool InitializeAndCheckListings(List<KeyWordsListingInfo> listingsList)
        {
            // validate and transform all parameters
            for (var index = 0; index < listingsList.Count; ++index)
            {
                var listingInfo = listingsList[index];
                if (listingInfo.ItemAction == ListingInfo.ListingAction.Skip)
                    continue;

                List<string> keyWordsArray = null;
                if (string.IsNullOrEmpty(listingInfo.KeyWords))
                {
                    OnErrorDuringPromotion(index, "Не задан список ключевых слов.");
                }
                else
                {
                    keyWordsArray = listingInfo.KeyWords.Split(';').ToList();
                    if (!keyWordsArray.Any())
                    {
                        keyWordsArray = null;
                        OnErrorDuringPromotion(index, $"Не удалось получить список ключевых слов из '{listingInfo.KeyWords}', проверьте формат.");
                    }
                }

                string listingId = null;
                try
                {
                    // link example https://www.etsy.com/listing/ID/... try extract ID
                    var match = Regex.Match(listingInfo.Link, "etsy.com/listing/(.*)/");
                    listingId = match.Groups[1].Value;
                }
                catch (Exception exception)
                {
                    if (!(exception is RegexMatchTimeoutException || exception is ArgumentException))
                        throw;

                    OnErrorDuringPromotion(index,
                        $"Не удалось получить идентификатор из ссылки '{listingInfo.Link}', проверьте что ссылка валидна и её формат https://www.etsy.com/listing/123123123/...\n\n" +
                        exception + "\n\n");
                }

                if (keyWordsArray != null && !string.IsNullOrEmpty(listingId))
                    _promotionList.Add(new PromotionInfo
                    {
                        AddToCard = listingInfo.ItemAction == ListingInfo.ListingAction.AddToCard,
                        ElementIndexInProductsList = index,
                        ListingId = listingId,
                        KeyWords = keyWordsArray.ToList()
                    });
            }

            return _promotionList.Any();
        }

        protected override SearchController CreateWebDriverController()
        {
            SearchController controller = new SearchController();
            controller.Driver.Manage().Window.Maximize();
            return controller;
        }

        protected override void ExecutePromotion(SearchController controller)
        {
            controller.OpenNewTab("https://www.etsy.com/");

            foreach (var promotionInfo in _promotionList)
            {
                foreach (var keyWord in promotionInfo.KeyWords)
                {
                    controller.SearchText(keyWord);

                    int currentPage = 0;
#if DEBUG
                    const int maxPageNumber = 3;
#else
                    const int maxPageNumber = 100;
#endif
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
                            allResults = controller.GetListOfSearchResults();
                        }
                        catch (NoSuchElementException) { }

                        try
                        {
                            productLink = controller.FindListingInSearchResults(promotionInfo.ListingId);
                        }
                        catch (NoSuchElementException) { }

                        ScrollSearchResults(controller, allResults, productLink);
                    } while (productLink == null && currentPage < maxPageNumber && controller.OpenNextSearchPage(currentPage + 1));

                    if (productLink != null)
                    {
                        try
                        {
                            controller.OpenInNewTab(productLink);
                        }
                        catch (WebDriverException exception)
                        {
                            OnErrorDuringPromotion(promotionInfo.ElementIndexInProductsList,
                                $"Не удалось открыть страницу с товаром, обратитесь к автору.\n\n {exception}");
                            continue;
                        }

                        InspectCurrentListing(controller, promotionInfo.ElementIndexInProductsList, promotionInfo.AddToCard);

                        try
                        {
                            controller.CloseCurrentTab();
                        }
                        catch (NoSuchWindowException)
                        {
                            Debug.Assert(false, "Почему-то не смогли закрыть вкладку хотя она была успешно открыта");
                        }
                    }
                    else
                    {
                        if (currentPage < 2)
                        {
                            OnErrorDuringPromotion(promotionInfo.ElementIndexInProductsList,
                                $"Не удалось найти товар по ключевому слову {keyWord}.\n" +
                                "Проверьте что по ключевым словам есть результаты поиска. " +
                                "Если есть много страниц с результатами значит программа не смогла переключить страницы, обратитесь к администратору.");
                        }
                        else
                        {
                            OnErrorDuringPromotion(promotionInfo.ElementIndexInProductsList, $"Не удалось найти товар по ключевому слову {keyWord}.");
                        }
                    }
                }
            }
        }

        private static void ScrollSearchResults(WebDriverHelper controller, List<IWebElement> allResults, IWebElement productLink)
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
            const int waitingTimeMilliseconds = 10;
#else
            const int waitingTimeMilliseconds = 1000;
#endif

            for (int currentIndex = 0, maxIndex = stopIndex ?? allResults.Count;
                currentIndex < maxIndex;
                currentIndex += 7)
            {
                if (controller.ScrollToElement(allResults[currentIndex]))
                    Thread.Sleep(waitingTimeMilliseconds);
            }

            if (stopIndex == null || stopIndex == -1)
                return;

            if (controller.ScrollToElement(allResults[stopIndex.Value]))
                Thread.Sleep(waitingTimeMilliseconds);
        }

        private class PromotionInfo
        {
            public bool AddToCard;
            public int ElementIndexInProductsList;
            public string ListingId;
            public List<string> KeyWords;
        }
    }
}
