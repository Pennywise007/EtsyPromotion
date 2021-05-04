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
        public String Presentation { get; set; }
        public ListingActionDetails Self { get { return this; } }

        public ListingActionDetails(ListingAction action, String presentation)
        {
            this.Action = action;
            this.Presentation = presentation;
        }

        public static void SetupListingActionsToColumn(ref DataGridViewComboBoxColumn column, out BindingList<ListingActionDetails> details)
        {
            details = new BindingList<ListingActionDetails>
            {
                new ListingActionDetails(ListingAction.Skip, "Пропускать"),
                new ListingActionDetails(ListingAction.AddToCard, "Добавлять в корзину"),
                new ListingActionDetails(ListingAction.Preview, "Только предпросмотр")
            };
            column.DataSource = details;
            column.DataPropertyName = "Action";
            column.DisplayMember = "Presentation";
            column.ValueMember = "Self";
        }
    }
}