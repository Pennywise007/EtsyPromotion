using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EtsyPromotion.Promotion.Interfaces;

namespace EtsyPromotion
{
    static class Globals
    {
        public static void HandleException(Exception exception, string title, IWin32Window window = null)
        {
            MessageBox.Show(window, exception.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Console.Error.WriteLine(exception.ToString());
        }

        public static string SettingsFileName = "settings.xml";
    }
}