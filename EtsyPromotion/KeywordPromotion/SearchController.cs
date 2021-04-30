using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace EtsyPromotion.KeywordPromotion
{
    class SearchController : EtsyController
    {
        public void SearchText(string text)
        {
            IWebElement mainSearchField = m_driver.FindElement(ByAttribute.Value("id", "global-enhancements-search-query", "input"));

            mainSearchField.Clear();

            mainSearchField.SendKeys(text);
            mainSearchField.SendKeys(Keys.Enter);
        }

        public List<IWebElement> getListOfSearchResults()
        {
            return GetSearchResultContext().FindElements(By.ClassName("js-merch-stash-check-listing")).ToList();
        }

        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        public IWebElement FindListingInSearchResults(string listingId)
        {
            return GetSearchResultContext().FindElement(ByAttribute.Value("data-listing-id", listingId));
        }

        public bool OpenNextSearchPage()
        {
            try
            {
                ISearchContext searchPagesGroup = m_driver.FindElement(ByAttribute.Name("data-search-pagination", "div"));
                List<IWebElement> pages = searchPagesGroup.FindElements(By.ClassName("wt-action-group__item-container")).ToList();
                if (pages.Any())
                {
                    if (pages.Last().Text != "Next")
                        throw new NoSuchElementException();

                    ScrollToElement(pages.Last());
                    pages.Last().Click();
                    return true;
                }
            }
            catch (NoSuchElementException) { }

            return false;
        }

        private ISearchContext GetSearchResultContext()
        {
            ISearchContext allResultsTable = m_driver;

            try
            {
                allResultsTable = allResultsTable.FindElement(ByAttribute.Name("data-search-results", "div"));
            }
            catch (NoSuchElementException) { }

            try
            {
                allResultsTable = allResultsTable.FindElement(ByAttribute.Name("data-search-results-lg", "div"));
            }
            catch (NoSuchElementException) { }

            return allResultsTable;
        }

    }
}
