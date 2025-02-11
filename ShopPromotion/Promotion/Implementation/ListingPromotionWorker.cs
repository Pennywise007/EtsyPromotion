using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShopPromotion.Promotion.Interfaces;
using ShopPromotion.Controller;
using OpenQA.Selenium;

namespace ShopPromotion.Promotion.Implementation
{
    internal class ListingPromotionWorker : PromotionWorkerBase<ListingInfo>, IListingPromotionWorker
    {
        /// <summary>
        /// Transformed listings info into promotion list
        /// </summary>
        private readonly List<PromotionInfo> _promotionList = new List<PromotionInfo>();

        protected override bool InitializeAndCheckListings(List<ListingInfo> listingsList, SiteMode siteMode)
        {
            _promotionList.Clear();

            // validate and transform all parameters
            for (var index = 0; index < listingsList.Count; ++index)
            {
                var listingInfo = listingsList[index];
                if (listingInfo.ItemAction == ListingInfo.ListingAction.Skip || string.IsNullOrEmpty(listingInfo.Link))
                    continue;

                Debug.Assert(listingInfo.ItemAction != ListingInfo.ListingAction.SearchOnly, "Search mode not supported by ListingPromotionWorker");

                _promotionList.Add(new PromotionInfo
                {
                    AddToCard = listingInfo.ItemAction == ListingInfo.ListingAction.AddToCard,
                    ElementIndexInProductsList = index,
                    ListingLink = listingInfo.Link
                });
            }

            return _promotionList.Any();
        }

        protected override IShopController CreateShopController(SiteMode siteMode)
        {
            IShopController controller = ControllerFactory.NewShopController(siteMode);
            controller.MaximizeWindow();
            return controller;
        }

        protected override void ExecutePromotion(IShopController controller)
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
