using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtsyPromotion.Promotion.Interfaces
{
    public enum RunMode
    {
        eOnes = 0,
        eMinutes_5,
        eMinutes_15,
        eHour_1,
        eHour_5,
        eDay,
        eUntilInterrupt
    }

    /// <summary>
    /// Interface for executing promotion on multiple listings
    /// </summary>
    /// <typeparam name="TListingInfoType">Class with info about listing<see cref="T:EtsyPromotion.Promotion.Interfaces.ListingInfo">ListingInfo</see></typeparam>
    public interface IPromotionWorker<TListingInfoType> : IDisposable
        where TListingInfoType : class
    {
        event EventHandler WhenStart;

        /// <summary>
        /// Finishing promotion
        /// <param> string is null if no error otherwise contain error message</param>
        /// </summary>
        event EventHandler<string> WhenFinish;

        /// <summary> Handle exception during promotion </summary>
        event EventHandler<Exception> WhenException;

        /// <summary> Finishing promotion on listing by index </summary>
        event EventHandler<PromotionDone> WhenFinishListingPromotion;

        /// <summary> An error occurred during the listing promotion </summary>
        event EventHandler<ErrorDuringListingPromotion> WhenErrorDuringListingPromotion;

        /// <summary> Delegate that some listings was promoted, calling before closing web driver after promotion </summary>
        /// <param>Count of promoted listings</param>
        /// <returns>Return true if need to close web driver, false otherwise </returns>
        event Func<int, bool> OnSuccessfullyPromoted;

        /// <returns> True if promotion started successfully </returns>
        bool StartPromotion(List<TListingInfoType> listingsList, RunMode runMode);

        /// <summary> Interrupting promotion </summary>
        void Interrupt();

        /// <returns>True if promotion executing</returns>
        bool IsWorking();
    }

    public class ElementInfo
    {
        public int ElementIndex;
    }

    public class ErrorDuringListingPromotion : ElementInfo
    {
        /// <summary>
        /// An error occurred during the listing promotion
        /// </summary>
        public string ErrorMessage;
    }

    public class PromotionDone : ElementInfo
    {
        private readonly DateTime _dateTime;

        public string Date => _dateTime.ToString("dd/MM/yyyy HH:mm");

        public PromotionDone(int listingIndex)
        {
            base.ElementIndex = listingIndex;
            _dateTime = DateTime.Now;
        }
    }

    // Helping class for getting the presentation of listing actions in lists
    public class RunModeDetails
    {
        public RunMode Mode { get; set; }
        public string Presentation { get; set; }

        public static void SetupRunModeToComboBox(ref MetroFramework.Controls.MetroComboBox combobox)
        {
            combobox.DataSource = new BindingList<RunModeDetails>
            {
                new RunModeDetails{ Mode = RunMode.eOnes, Presentation = "Один раз" },
                new RunModeDetails{ Mode = RunMode.eMinutes_5, Presentation = "Раз в 5 минут" },
                new RunModeDetails{ Mode = RunMode.eMinutes_15, Presentation = "Раз в 15 минут" },
                new RunModeDetails{ Mode = RunMode.eHour_1, Presentation = "Раз в час" },
                new RunModeDetails{ Mode = RunMode.eHour_5, Presentation = "Раз в 5 часов" },
                new RunModeDetails{ Mode = RunMode.eDay, Presentation = "Раз в 5 день" },
                new RunModeDetails{ Mode = RunMode.eUntilInterrupt, Presentation = "До отмены" }
            };
            combobox.DisplayMember = "Presentation";
            combobox.ValueMember = "Mode";
        }
    }
}
