using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EtsyPromotion.Promotion.Interfaces
{
    public interface IKeyWordPromotionWorker : IPromotionWorker<KeyWordsListingInfo>
    {
       void SetMaxSearchPagesCount(int maxSearchPagesCount);

        /// <summary> Generates on successful finding of the listing by tag </summary>
        event EventHandler<FoundListingInfo> WhenFoundListing;
    }

    public class FoundListingInfo : ElementInfo
    {
        // Page index of the page on which the item was found, used in key word promotion
        public readonly int PageIndex;
        public FoundListingInfo(int listingIndex, int pageIndex)
        {
            base.ElementIndex = listingIndex;
            PageIndex = pageIndex;
        }
    }

    public class KeyWordsListingInfo : ListingInfo
    {
        public string KeyWords { get; set; }
        public string FoundOnPage { get; set; }
    }
}
