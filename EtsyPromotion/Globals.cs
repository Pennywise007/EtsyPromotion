using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EtsyPromotion
{
    static class Globals
    {
        public static void HandleException(Exception exception, string title)
        {
            MessageBox.Show(exception.ToString(), title);
            Console.Error.WriteLine(exception.ToString());
        }

        public static string g_settingsFileName = "settings.xml";
    }

    // Helping class for getting the presentation of listing actions in lists
    public class ListingActionDetails
    {
        // Action on an object during promotion
        public enum ListingAction
        {
            Skip = 0,
            AddToCard,
            Preview
        }

        public ListingAction Action { get; set; }
        public string Presentation { get; set; }

        public static void SetupListingActionsToColumn(ref DataGridViewComboBoxColumn column)
        {
            column.DataSource = new BindingList<ListingActionDetails>
            {
                new ListingActionDetails{ Action = ListingAction.Skip, Presentation = "Пропускать" },
                new ListingActionDetails{ Action = ListingAction.AddToCard, Presentation = "Добавлять в корзину" },
                new ListingActionDetails{ Action = ListingAction.Preview, Presentation = "Предосмотр(фото + комментарии)" }
            };
            column.DisplayMember = "Presentation";
            column.ValueMember = "Action";
        }
    }
}