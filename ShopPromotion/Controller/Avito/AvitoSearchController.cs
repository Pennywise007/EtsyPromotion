using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;

namespace ShopPromotion.Controller.Avito
{
    class SearchController : Controller, IShopSearchController
    {
        public void SearchText(string text)
        {
            var mainSearchField = WaitForElement(ByAttribute.Value("data-marker", "search-form/suggest/input", "input"), TimeSpan.FromSeconds(5));
            mainSearchField.Clear();
            mainSearchField.Click();

            Thread.Sleep(1000);

            foreach (var ch in text)
            {
                mainSearchField.SendKeys($"{ch}");
                Thread.Sleep(30);
            }

            Thread.Sleep(300);
            mainSearchField.SendKeys(Keys.Enter);
        }

        public List<IWebElement> GetListOfSearchResults()
        {
            return GetSearchResultContext().FindElements(By.XPath(".//a[contains(@class, 'iva-item-sliderLink')]")).ToList();
        }

        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        public IWebElement FindListingInSearchResults(string listingId)
        {
            return GetSearchResultContext().FindElement(By.XPath($".//a[contains(@href, '{listingId}')]"));
        }

        public bool OpenNextSearchPage(int nextPageNumber)
        {
            ISearchContext searchPagesGroup;
            try
            {
                searchPagesGroup = Driver.FindElement(ByAttribute.Value("data-marker", "pagination-button"));
            }
            catch (NoSuchElementException)
            {
                return false;
            }

            List<IWebElement> switchPageButtonGroups = searchPagesGroup.FindElements(ByAttribute.Name("data-value", "a")).ToList();
            if (!switchPageButtonGroups.Any())
                return false;

            for (int index = switchPageButtonGroups.Count - 1; index >= 0; --index)
            {
                var pageGroup = switchPageButtonGroups[index];
                var buttonIndex = pageGroup.GetAttribute("data-value");

                int pageNumber = int.Parse(buttonIndex);
                if (nextPageNumber != pageNumber)
                    continue;

                ScrollToElement(pageGroup);
                pageGroup.Click();
                return true;
            }

            return false;
        }

        private ISearchContext GetSearchResultContext()
        {
            try
            {
                return Driver.FindElement(By.XPath(".//div[contains(@class, 'items-items')]"));
            }
            catch (NoSuchElementException) { }

            return Driver;
        }
    }
}
