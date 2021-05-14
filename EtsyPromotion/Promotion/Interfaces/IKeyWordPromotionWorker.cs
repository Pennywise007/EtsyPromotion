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
    }

    public class KeyWordsListingInfo : ListingInfo
    {
        public string KeyWords { get; set; }
    }
}
