using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EtsyPromotion.Promotion.Interfaces;
using EtsyPromotion.WebDriver;
using OpenQA.Selenium;

namespace EtsyPromotion.Promotion.Implementation
{
    internal class ListingPromotionWorker : PromotionWorkerBase<ListingInfo, EtsyController>, IListingPromotionWorker
    {
        /// <summary>
        /// Transformed listings info into promotion list
        /// </summary>
        private readonly List<PromotionInfo> _promotionList = new List<PromotionInfo>();

        protected override bool InitializeAndCheckListings(List<ListingInfo> listingsList)
        {
            // validate and transform all parameters
            for (var index = 0; index < listingsList.Count; ++index)
            {
                var listingInfo = listingsList[index];
                if (listingInfo.ItemAction == ListingInfo.ListingAction.Skip || string.IsNullOrEmpty(listingInfo.Link))
                    continue;

                _promotionList.Add(new PromotionInfo
                {
                    AddToCard = listingInfo.ItemAction == ListingInfo.ListingAction.AddToCard,
                    ElementIndexInProductsList = index,
                    ListingLink = listingInfo.Link
                });
            }

            return _promotionList.Any();
        }

        protected override EtsyController CreateWebDriverController()
        {
            EtsyController controller = new EtsyController();
            controller.Driver.Manage().Window.Maximize();

            return controller;
        }

        protected override void ExecutePromotion(EtsyController controller)
        {
            foreach (var linkInfo in _promotionList)
            {
                try
                {
                    controller.OpenNewTab(linkInfo.ListingLink);
                }
                catch (WebDriverException exception)
                {
                    OnErrorDuringPromotion(linkInfo.ElementIndexInProductsList, $"Не удалось открыть ссылку {linkInfo.ListingLink}.\n\n{exception}");
                    continue;
                }

                InspectCurrentListing(controller, linkInfo.ElementIndexInProductsList, linkInfo.AddToCard);
            }
        }

        private class PromotionInfo
        {
            public bool AddToCard;
            public int ElementIndexInProductsList;
            public string ListingLink;
        }
    }
}
