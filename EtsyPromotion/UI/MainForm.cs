using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using EtsyPromotion.Promotion.Interfaces;
using EtsyPromotion.WebDriver;
using MetroFramework.Forms;
using Microsoft.Extensions.DependencyInjection;
using EtsyPromotion.General;
using System.Net.Sockets;

namespace EtsyPromotion.UI
{
    public partial class MainForm : MetroForm
    {
        private Settings _settings = new Settings();
        private readonly PromotionTableStatusManager<ListingInfo> _statusManager;

        private readonly List<string> _listIPs = new List<string>();
        private readonly SortedDictionary<int, IKeyWordPromotionForm> _keyWordPromotionWindows = new SortedDictionary<int, IKeyWordPromotionForm>();

        private readonly CancellationTokenSource _updateIpCancellationToken = new CancellationTokenSource();

        private readonly IServiceProvider _serviceProvider;
        private readonly IListingPromotionWorker _listingPromotionWorker;

        public MainForm(IServiceProvider serviceProvider, IListingPromotionWorker listingPromotionWorker)
        {
            _serviceProvider = serviceProvider;
            _listingPromotionWorker = listingPromotionWorker;

            InitializeComponent();
            InitializeWorker();

            ListingActionDetails.SetupListingActionsToColumn(ref listingActionColumn);
            RunModeDetails.SetupRunModeToComboBox(ref RunModeComboBox);
            SiteModeDetails.SetupSiteModeToComboBox(ref SiteModeComboBox);

            LoadSettingsFromXML();
            RunModeComboBox.SelectedIndex = (int)_settings.RunMode;
            SiteModeComboBox.SelectedIndex = (int)_settings.SiteMode;

            ItemsTable.DataSource = new BindingSource
            {
                DataSource = new SortableBindingList<ListingInfo>(_settings.ListingsList),
                AllowNew = true
            };

            _statusManager = new PromotionTableStatusManager<ListingInfo>(_listingPromotionWorker, ItemsTable,
                                                                          listingActionColumn, PromotionStatusColumn);
        }

        /// <summary>
        /// Initialize worker and add subscription to events
        /// </summary>
        private void InitializeWorker()
        {
            _listingPromotionWorker.WhenStart += OnStartPromotion;
            _listingPromotionWorker.WhenFinish += OnFinishPromotion;
            _listingPromotionWorker.WhenException += OnException;
            _listingPromotionWorker.WhenFinishListingPromotion += OnFinishListingPromotion;
            _listingPromotionWorker.OnSuccessfullyPromoted += OnSuccessfullyPromoted;
        }

#region WorkerEvents
        private void OnStartPromotion(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(() =>
            {
                ItemsTable.Enabled = false;
                RunModeComboBox.Enabled = false;
                SiteModeComboBox.Enabled = false;
                Button_RunPromotion.Text = "Прервать продвижение";
            }));
        }

        private void OnFinishPromotion(object sender, string error)
        {
            Invoke(new MethodInvoker(() =>
            {
                ItemsTable.Enabled = true;
                RunModeComboBox.Enabled = true;
                SiteModeComboBox.Enabled = true;

                Button_RunPromotion.Enabled = true;
                Button_RunPromotion.Text = "Запустить продвижение";

                if (!string.IsNullOrEmpty(error))
                    MessageBox.Show(this, error, "Во время выполнения продвижения возникли ошибки", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }));
        }

        private void OnException(object sender, Exception exception)
        {
            Invoke(new MethodInvoker(() =>
            {
                SetForeground();

                Globals.HandleException(exception, "Возникла критическая ошибка во время продвижения.", this);
            }));
        }

        private void OnFinishListingPromotion(object sender, PromotionDone doneInfo)
        {
            Invoke(new MethodInvoker(() =>
            {
                _settings.ListingsList[doneInfo.ElementIndex].DateLastPromotion = doneInfo.Date;
                ItemsTable.Refresh();
            }));
        }

        private bool OnSuccessfullyPromoted(int countPromotedElements)
        {
            bool closeWindow = false;

            Invoke(new MethodInvoker(() =>
            {
                SetForeground();
                closeWindow = MessageBox.Show(this, $"Продвижение для {countPromotedElements} товаров было успешно. Закрыть открытые окна?",
                    "Продвижение завершено", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes;

                if (countPromotedElements == 0)
                    return;

                var myIp = GetCurrentIP();
                if (!string.IsNullOrEmpty(myIp))
                    _listIPs.Add(myIp);
            }));

            return closeWindow;
        }
#endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (_keyWordPromotionWindows.Any())
            {
                var previousOpenedFormsCount = _keyWordPromotionWindows.Count;
                _keyWordPromotionWindows.First().Value.CloseForm();
                bool formClosed = previousOpenedFormsCount > _keyWordPromotionWindows.Count;
                if (!formClosed)
                {
                    e.Cancel = true;
                    break;
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _settings.RunMode = (RunModeComboBox.SelectedItem as RunModeDetails).Mode;
            _settings.SiteMode = (SiteModeComboBox.SelectedItem as SiteModeDetails).Mode;
            SaveSettingsToXML();

            _updateIpCancellationToken.Cancel();
        }

        private void UpgradeSettingsFile()
        {
            var upgrader = new XmlSettingsUpgrader(Globals.SettingsFileName);

            try
            {
                int? currentSettingsVersion = upgrader.GetCurrentSettingsVersion();

                var settingsVersion = currentSettingsVersion;
                if (settingsVersion == null)
                {
                    upgrader.RenameAllNodes("ArrayOfEtsyLinkInfo", "Settings");
                    upgrader.RenameAllNodes("EtsyLinkInfo", "ListingInfo");
                    upgrader.AddNewSeparatorNodeBetweenNodes("Settings", "ListingInfo", "ListingsList");

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
            UpgradeSettingsFile();

            try
            {
                var smlSerializer = new XmlSerializer(typeof(Settings));
                using (var rd = new StreamReader(Globals.SettingsFileName))
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
            using (var wr = new StreamWriter(Globals.SettingsFileName))
            {
                smlSerializer.Serialize(wr, _settings);
            }
        }

        private void Button_CheckLocation_Click(object sender, EventArgs e)
        {
            var etsyController = new EtsyController();

            string etsyUserLocation = null;
            try
            {
                etsyUserLocation = etsyController.GetEtsyUserLocation();
            }
            catch (Exception exception)
            {
                Globals.HandleException(exception, "Ошибка при получении местоположения");
            }

            string ipLocation = null;
            string ip = null;
            try
            {
                ipLocation = etsyController.IpLocation;
                ip = etsyController.Ip;
            }
            catch (Exception exception)
            {
                if (etsyUserLocation != null)
                    Globals.HandleException(exception, "Ошибка при определении местоположения");
            }

            try
            {
                etsyController.Driver.Quit();
            }
            catch (Exception)
            {
            }

            string GetStringPresentation(string val)
            {
                return val ?? "не удалось определить";
            }

            MessageBox.Show($"Местоположение пользователя определённое на etsty.com: {GetStringPresentation(etsyUserLocation)}.\r\nМестоположение определённое через сторонние сервисы: {GetStringPresentation(ipLocation)}.\r\nIP: {GetStringPresentation(ip)}",
                            "Результат определения местоположения");
        }

        private void SetForeground()
        {
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
            else
            {
                TopMost = true;
                Focus();
                BringToFront();
                TopMost = false;
            }
        }

        private void Button_AddItemsToCard_Click(object sender, EventArgs e)
        {
            if (_listingPromotionWorker.IsWorking())
            {
                Button_RunPromotion.Enabled = false;
                Button_RunPromotion.Text = "Прерываем продвижение...";

                _listingPromotionWorker.Interrupt();

                return;
            }

            var myIp = GetCurrentIP();

            if (!string.IsNullOrEmpty(myIp) && _listIPs.Contains(myIp) &&
                MessageBox.Show($"Во время работы программы уже добавлялись товары с этого IP {myIp}, может вы забыли поменять его. Выполнить продвижение?",
                    "Проверка IP", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

            _listingPromotionWorker.StartPromotion(_settings.ListingsList, (RunModeComboBox.SelectedItem as RunModeDetails).Mode, (SiteModeComboBox.SelectedItem as SiteModeDetails).Mode);
        }

        private void ItemsTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == ItemsTable.Columns.IndexOf(openLinkColumn))
            {
                if (e.RowIndex < _settings.ListingsList.Count)
                {
                    try
                    {
                        Process.Start(_settings.ListingsList[e.RowIndex].Link);
                    }
                    catch (SystemException exception)
                    {
                        Globals.HandleException(exception, "Не удалось перейти по ссылке");
                    }
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var cancellationToken = _updateIpCancellationToken.Token;
            Task.Run(() =>
            {
                try
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        GetCurrentIP(true);

                        cancellationToken.WaitHandle.WaitOne(TimeSpan.FromSeconds(20));
                    }
                }
                catch (TaskCanceledException)
                {}
            }, cancellationToken);
        }

        private void CurrentIP_Click(object sender, EventArgs e)
        {
            GetCurrentIP(true);
        }

        private string GetCurrentIP(bool updateTextControl = false)
        {
            string currentIp = null;
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        currentIp = ip.ToString();
                        break;
                    }
                }
            }
            catch (Exception)
            {
            }

            if (updateTextControl)
            {
                CurrentIP.BeginInvoke(new MethodInvoker(() =>
                {
                    CurrentIP.Text = "Текущий IP: " + (string.IsNullOrEmpty(currentIp) ? "Не удалось определить" : currentIp);
                }));
            }

            return currentIp;
        }

        private void Button_KeyWordPromotion_Click(object sender, EventArgs e)
        {
            IKeyWordPromotionForm newKeyWordPromotionForm = _serviceProvider.GetRequiredService<IKeyWordPromotionForm>();

            var minimumNotOpenedWindowIndex = 0;
            foreach (var windowIndex in _keyWordPromotionWindows.Keys)
            {
                if (windowIndex > minimumNotOpenedWindowIndex)
                    break;

                if (windowIndex == minimumNotOpenedWindowIndex)
                    ++minimumNotOpenedWindowIndex;
            }

            newKeyWordPromotionForm.InitializeForm(minimumNotOpenedWindowIndex);
            newKeyWordPromotionForm.WhenFormClosed += delegate (Object o, int windowIndex)
            {
                try
                {
                    if (!_keyWordPromotionWindows.Remove(windowIndex))
                        throw new ArgumentOutOfRangeException("Failed to find window by index");
                }
                catch (ArgumentOutOfRangeException)
                {
                    Debug.Assert(false);
                }
            };

            _keyWordPromotionWindows.Add(minimumNotOpenedWindowIndex, newKeyWordPromotionForm);
        }

        private void ItemsTable_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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
            public List<ListingInfo> ListingsList = new List<ListingInfo>();
        }
    }
}