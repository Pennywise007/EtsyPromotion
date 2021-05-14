using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroFramework.Forms;

namespace EtsyPromotion.UI
{
    public interface IKeyWordPromotionForm
    {
        /// <summary>
        /// Event about closing window, called with window index
        /// <see cref="T:InitializeForm">InitializeForm(windowIndex)</see>
        /// </summary>
        event EventHandler<int> WhenFormClosed;

        /// <summary>
        /// Initializing form, installing window index and callback
        /// </summary>
        /// <param name="windowIndex">Index of window, used for finding window settings</param>
        void InitializeForm(int windowIndex);

        void CloseForm();
    }
}
