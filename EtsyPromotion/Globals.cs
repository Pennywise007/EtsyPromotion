using System;
using System.Collections.Generic;
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
}
