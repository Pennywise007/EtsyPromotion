using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using EtsyPromotion.UI;
using EtsyPromotion.General;
using EtsyPromotion.Promotion.Interfaces;
using MetroFramework.Forms;

namespace EtsyPromotion.UI
{
    public partial class KeywordPromotionForm : MetroForm, IKeyWordPromotionForm
    {
        public KeywordPromotionForm(IKeyWordPromotionWorker worker)
        {
            _worker = worker;

            InitializeComponent();
            InitializeWorker();

            ListingActionDetails.SetupListingActionsToColumn(ref listingActionColumn);
        }

        /// <summary>
        /// Event about closing window, called with window index
        /// <see cref="T:InitializeForm">InitializeForm(windowIndex)</see>
        /// </summary>
        public event EventHandler<int> WhenFormClosed;

        /// <summary>
        /// Initializing form, installing window index and callback
        /// </summary>
        /// <param name="windowIndex">Index of window, used for finding window settings</param>
        public void InitializeForm(int windowIndex)
        {
            _windowIndex = windowIndex;

            LoadSettingsFromXML();

            PromotionList.DataSource = new BindingSource
            {
                DataSource = _settings.ProductsList,
                AllowNew = true
            };

            Show();
        }

        public void CloseForm()
        {
            Close();
        }

        /// <summary>
        /// Initialize worker and add subscription to events
        /// </summary>
        private void InitializeWorker()
        {
            _worker.WhenStart += OnStartPromotion;
            _worker.WhenFinish += OnFinishPromotion;
            _worker.WhenException += OnException;
            _worker.WhenFinishListingPromotion += OnFinishListingPromotion;
            _worker.WhenErrorDuringListingPromotion += OnErrorDuringListingPromotion;
        }

#region WorkerEvents
        private void OnStartPromotion(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(() =>
            {
                PromotionList.Enabled = false;
                Button_StartPromotion.Text = "Прервать продвижение";
            }));
        }

        private void OnFinishPromotion(object sender, string error)
        {
            Invoke(new MethodInvoker(() =>
            {
                PromotionList.Enabled = true;
                Button_StartPromotion.Enabled = true;
                Button_StartPromotion.Text = "Запустить продвижение";

                if (!string.IsNullOrEmpty(error))
                    MessageBox.Show(this, error, "Во время выполнения продвижения возникли ошибки", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }));
        }

        private void OnException(object sender, Exception exception)
        {
            Invoke(new MethodInvoker(() =>
            {
                Globals.HandleException(exception, "Возникла критическая ошибка во время продвижения.", this);
            }));
        }

        private void OnFinishListingPromotion(object sender, int listingIndex)
        {
            Invoke(new MethodInvoker(() =>
            {
                _settings.ProductsList[listingIndex].DateLastPromotion = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                PromotionList.Refresh();
            }));
        }

        private void OnErrorDuringListingPromotion(object sender, ErrorDuringListingPromotion errorInfo)
        {
            Invoke(new MethodInvoker(() =>
            {
               // ProductsList[listingIndex].DateLastPromotion = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
               // PromotionList.Refresh();

                // TODO add HANDLER
            }));
        }
#endregion

        private void KeywordPromotion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_worker.IsWorking() &&
                MessageBox.Show("Если закрыть окно то продвижение указанных товаров приостановится, продолжить закрытие?",
                    "Закрытие окна",
                    MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;
        }

        private void KeywordPromotion_FormClosed(object sender, FormClosedEventArgs e)
        {
            _worker.Dispose();

            SaveSettingsToXML();

            WhenFormClosed?.Invoke(this, _windowIndex);
        }

        private string GetCurrentWindowSettingsFilePath()
        {
            return $"promotion_{_windowIndex}.xml";
        }

        private void UpgradeSettingsFile()
        {
            var upgrader = new XmlSettingsUpgrader(GetCurrentWindowSettingsFilePath());

            try
            {
                int? settingsVersion = upgrader.GetCurrentSettingsVersion();
                if (settingsVersion == null)
                {
                    upgrader.RenameAllNodes("ArrayOfProductsListItem", "Settings");
                    upgrader.RenameAllNodes("ProductsListItem", "KeyWordsListingInfo");

                    upgrader.AddNewSeparatorNodeBetweenNodes("Settings", "KeyWordsListingInfo", "ProductsList");

                    upgrader.AddAttributeToNodes("Settings", "SettingsVersion", ((int)Settings.Versions.eCurrent).ToString());
                }
                else
                {
                    if (settingsVersion.Value < (int)Settings.Versions.eCurrent)
                    {
                        Debug.Assert(false, "Пока есть только первая версия настроек");
                    }
                }
            }
            finally
            {
                upgrader.Dispose();
            }
        }

        private void LoadSettingsFromXML()
        {
            UpgradeSettingsFile();

            try
            {
                var smlSerializer = new XmlSerializer(typeof(Settings));
                using (var rd = new StreamReader(GetCurrentWindowSettingsFilePath()))
                {
                    _settings = smlSerializer.Deserialize(rd) as Settings;
                }
            }
            catch (FileNotFoundException)
            {
            }
            catch (Exception exception)
            {
                Globals.HandleException(exception, "Ошибка при загрузке настроек");
            }
        }

        private void SaveSettingsToXML()
        {
            _settings.SettingsVersion = (int)Settings.Versions.eCurrent;
            var smlSerializer = new XmlSerializer(typeof(Settings));
            using (var wr = new StreamWriter(GetCurrentWindowSettingsFilePath()))
            {
                smlSerializer.Serialize(wr, _settings);
            }
        }

        private void Button_StartPromotion_Click(object sender, EventArgs e)
        {
            if (_worker.IsWorking())
            {
                Button_StartPromotion.Enabled = false;
                Button_StartPromotion.Text = "Прерываем продвижение...";

                _worker.Interrupt();
                return;
            }

            _worker.StartPromotion(_settings.ProductsList);
        }

        // index of window settings
        private int _windowIndex;
        private Settings _settings = new Settings();
        private readonly IKeyWordPromotionWorker _worker;

        public class Settings : ProgramSettings
        {
            // Settings version
            public enum Versions
            {
                eCurrent = 1
            }

            public BindingList<KeyWordsListingInfo> ProductsList = new BindingList<KeyWordsListingInfo>();
        }
    }
}