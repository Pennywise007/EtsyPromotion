using OpenQA.Selenium;
using System.Collections.Generic;
using ShopPromotion.Promotion.Interfaces;
using System;

namespace ShopPromotion.Controller
{
    // Interfaces to control the browser
    public interface IBrowserController
    {
        // get current IP
        string GetIp();
        // Get current location based on IP
        string GetIpLocation();
        /// <exception cref="T:OpenQA.Selenium.NoSuchWindowException">If the window cannot be found.</exception>
        void OpenNewTab(string newUrl);
        // Open in current browser window
        void OpenInCurrentWindow(string newUrl);
        // Close everything
        void Quit();
        // maximize browser window
        void MaximizeWindow();
        /// <exception cref="T:WebDriverException">.</exception>
        void OpenInNewTab(IWebElement element);
        /// <exception cref="T:OpenQA.Selenium.NoSuchWindowException">If the window cannot be found.</exception>
        void CloseCurrentTab();
        // Navigate back, to prev page
        void Back();
        // Make element visible
        bool ScrollToElement(IWebElement element);
        // Scroll the window
        void ScrollTo(int xPosition = 0, int yPosition = 0);
        // Scroll current page to the end
        void ScrollToThePageEnd();
    }

    // Interface to do some manipulations with the shop
    public interface IShopController : IBrowserController
    {
        void OpenMainShopPage();

        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        void AddCurrentItemToCart();

        /// <exception cref="T:OpenQA.Selenium.WebDriverException">If no element matches the criteria.</exception>
        void PreviewPhotos();

        /// <exception cref="T:OpenQA.Selenium.WebDriverException">If no element matches the criteria.</exception>
        void WatchComments(int maxPagesToWatch);

        List<IWebElement> GetSuggestionsFromCurrentShop();

        List<IWebElement> GetShopListingsList(bool loadAll);

        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        IWebElement FindSellerLink();

        /// <exception cref="T:OpenQA.Selenium.NotFoundException">If no element matches the criteria.</exception>
        string GetUserLocation();
    }

    // Interface to do some manipulations with the shop search
    public interface IShopSearchController : IShopController
    {
        void SearchText(string text);

        List<IWebElement> GetListOfSearchResults();

        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        IWebElement FindListingInSearchResults(string listingId);

        bool OpenNextSearchPage(int nextPageNumber);
    }

    public class ControllerFactory
    {
        public static IShopController NewShopController(SiteMode siteMode)
        {
            switch (siteMode)
            {
                case SiteMode.eAvito:
                    return new Avito.Controller();
                case SiteMode.eEtsy:
                    return new Etsy.Controller();
                default:
                    throw new ArgumentOutOfRangeException("unknown site mode");
            }
        }

        public static IShopSearchController NewShopSearchController(SiteMode siteMode)
        {
            switch (siteMode)
            {
                case SiteMode.eAvito:
                    return new Avito.SearchController();
                case SiteMode.eEtsy:
                    return new Etsy.SearchController();
                default:
                    throw new ArgumentOutOfRangeException("unknown site mode");
            }
        }
    }
}
