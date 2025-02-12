using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using ShopPromotion.UI;
using ShopPromotion.General;
using ShopPromotion.Promotion.Interfaces;
using MetroFramework.Forms;

namespace ShopPromotion.UI
{
    public partial class KeywordPromotionForm : MetroForm, IKeyWordPromotionForm
    {
        public KeywordPromotionForm(IKeyWordPromotionWorker worker)
        {
            _worker = worker;

            InitializeComponent();
            InitializeWorker();

            ListingActionDetails.SetupListingActionsToColumn(ref listingActionColumn, true);
            RunModeDetails.SetupRunModeToComboBox(ref RunModeComboBox);
            SiteModeDetails.SetupSiteModeToComboBox(ref SiteModeComboBox);

            _statusManager = new PromotionTableStatusManager<KeyWordsListingInfo>(_worker, PromotionList,
                                                                                  listingActionColumn, StatusColumn);
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
                DataSource = new SortableBindingList<KeyWordsListingInfo>(_settings.ProductsList),
                AllowNew = true
            };

            RunModeComboBox.SelectedIndex = (int)_settings.RunMode;
            SiteModeComboBox.SelectedIndex = (int)_settings.SiteMode;
            MaximumSearchPagesNumericUpDown.Value = _settings.MaximumSearchPages;

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
            _worker.WhenFoundListing += OnWhenFoundListing;
        }

#region WorkerEvents
        private void OnStartPromotion(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(() =>
            {
                PromotionList.Enabled = false;
                RunModeComboBox.Enabled = false;
                SiteModeComboBox.Enabled = false;
                MaximumSearchPagesNumericUpDown.Enabled = false;

                Button_StartPromotion.Text = "Прервать продвижение";
            }));
        }

        private void OnFinishPromotion(object sender, string error)
        {
            Invoke(new MethodInvoker(() =>
            {
                PromotionList.Enabled = true;
                RunModeComboBox.Enabled = true;
                SiteModeComboBox.Enabled = true;
                MaximumSearchPagesNumericUpDown.Enabled = true;

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

        private void OnFinishListingPromotion(object sender, PromotionDone promotionDone)
        {
            Invoke(new MethodInvoker(() =>
            {
                _settings.ProductsList[promotionDone.ElementIndex].DateLastPromotion = promotionDone.Date;

                PromotionList.Refresh();
            }));
        }

        private void OnWhenFoundListing(object sender, FoundListingInfo foundInfo)
        {
            Invoke(new MethodInvoker(() =>
            {
                _settings.ProductsList[foundInfo.ElementIndex].FoundOnPage = foundInfo.PageIndex;
                PromotionList.Refresh();
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

            _settings.RunMode = (RunModeComboBox.SelectedItem as RunModeDetails).Mode;
            _settings.SiteMode = (SiteModeComboBox.SelectedItem as SiteModeDetails).Mode;
            _settings.MaximumSearchPages = (int)MaximumSearchPagesNumericUpDown.Value;

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
                int? currentSettingsVersion = upgrader.GetCurrentSettingsVersion();

                var settingsVersion = currentSettingsVersion;
                if (settingsVersion == null)
                {
                    upgrader.RenameAllNodes("ArrayOfProductsListItem", "Settings");
                    upgrader.RenameAllNodes("ProductsListItem", "KeyWordsListingInfo");
                    upgrader.AddNewSeparatorNodeBetweenNodes("Settings", "KeyWordsListingInfo", "ProductsList");

                    settingsVersion = (int)Settings.Versions.eKeywordsPromotion;
                }

                if (settingsVersion != currentSettingsVersion)
                    upgrader.AddAttributeToNodes("Settings", "SettingsVersion", settingsVersion.ToString());

                Debug.Assert(settingsVersion == (int)Settings.Versions.eCurrent, "Not updated settings version");
            }
            finally
            {
                upgrader.Dispose();
            }
        }

        private void LoadSettingsFromXML()
        {
            var settingFile = GetCurrentWindowSettingsFilePath();

            if (!File.Exists(settingFile))
                return;

            UpgradeSettingsFile();

            try
            {
                var smlSerializer = new XmlSerializer(typeof(Settings));
                using (var rd = new StreamReader(settingFile))
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

            _worker.SetMaxSearchPagesCount((int) MaximumSearchPagesNumericUpDown.Value);
            _worker.StartPromotion(_settings.ProductsList, (RunModeComboBox.SelectedItem as RunModeDetails).Mode, (SiteModeComboBox.SelectedItem as SiteModeDetails).Mode);
        }

        private void PromotionList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // draw row indexes
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        // index of window settings
        private int _windowIndex;
        private Settings _settings = new Settings();
        private readonly PromotionTableStatusManager<KeyWordsListingInfo> _statusManager;
        private readonly IKeyWordPromotionWorker _worker;

        public class Settings
        {
            // Settings version
            public enum Versions
            {
                eInitial = 0,
                eKeywordsPromotion,

                // Add new versions here
                eLast,
                eCurrent = eLast - 1
            }
            [XmlAttribute] public int SettingsVersion = (int)Versions.eCurrent;

            public SiteMode SiteMode = SiteMode.eAvito;
            public RunMode RunMode = RunMode.eOnes;
            public int MaximumSearchPages = 100;
            public List<KeyWordsListingInfo> ProductsList = new List<KeyWordsListingInfo>();
        }
    }
}