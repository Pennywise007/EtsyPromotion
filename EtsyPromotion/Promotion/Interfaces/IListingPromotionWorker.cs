﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EtsyPromotion.Promotion.Interfaces
{
    public interface IListingPromotionWorker : IPromotionWorker<ListingInfo>
    {
    }

    public class ListingInfo
    {
        // Action on an object during promotion
        public enum ListingAction
        {
            Skip = 0,
            AddToCard,
            Preview
        }

        public ListingAction ItemAction { get; set; } = ListingAction.AddToCard;
        public string Link { get; set; }
        public string DateLastPromotion { get; set; }
        public string Note { get; set; }
    }

    // Helping class for getting the presentation of listing actions in lists
    public class ListingActionDetails
    {
        public ListingInfo.ListingAction Action { get; set; }
        public string Presentation { get; set; }

        public static void SetupListingActionsToColumn(ref DataGridViewComboBoxColumn column)
        {
            column.DataSource = new BindingList<ListingActionDetails>
            {
                new ListingActionDetails{ Action = ListingInfo.ListingAction.Skip,      Presentation = "Пропускать" },
                new ListingActionDetails{ Action = ListingInfo.ListingAction.AddToCard, Presentation = "Добавлять в корзину" },
                new ListingActionDetails{ Action = ListingInfo.ListingAction.Preview,   Presentation = "Предосмотр(фото + комментарии)" }
            };
            column.DisplayMember = "Presentation";
            column.ValueMember = "Action";
        }
    }

}
