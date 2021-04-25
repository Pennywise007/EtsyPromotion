using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using MetroFramework.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EtsyPromotion
{
    public partial class MainForm : MetroForm
    {
        private List<EtsyLinkInfo> m_list = new List<EtsyLinkInfo>();
        private List<string> m_listIPs = new List<string>();
        private Thread m_updateIpThread;

        public MainForm()
        {
            InitializeComponent();

            LoadSettingsFromXML();

            ItemsTable.DataSource = new BindingSource
            {
                DataSource = m_list,
                AllowNew = true
            };
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettingsToXML();

            m_updateIpThread.Interrupt();
        }

        private void SaveSettingsToXML()
        {
            var smlSerializer = new XmlSerializer(typeof(List<EtsyLinkInfo>));
            using (var wr = new StreamWriter(Globals.g_settingsFileName))
            {
                smlSerializer.Serialize(wr, m_list);
            }
        }

        private void LoadSettingsFromXML()
        {
            try
            {
                var smlSerializer = new XmlSerializer(typeof(List<EtsyLinkInfo>));
                using (var rd = new StreamReader(Globals.g_settingsFileName))
                {
                    m_list = smlSerializer.Deserialize(rd) as List<EtsyLinkInfo>;
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

        private EtsyController createEtsyController()
        {
            // start new chrome as incognito
            var options = new ChromeOptions();
            //options.AddArguments("--incognito");

            return new EtsyController(new ChromeDriver(options));
        }

        private void Button_CheckLocation_Click(object sender, EventArgs e)
        {
            EtsyController etsyController = createEtsyController();
            etsyController.m_driver.Manage().Window.Maximize();

            etsyController.OpenNewTab(
                "https://www.etsy.com/listing/1006447031/custom-rubber-keychains?ref=related-3");

            var listSuggestions = etsyController.GetSuggestionsFromThisShop();

            foreach (var suggestion in listSuggestions)
            {
                etsyController.OpenInNewTab(suggestion);
                //suggestion.Click();

                //etsyController.Back();
            }



            //etsyController.PreviewPhotos();

            //etsyController.WatchComments();

            //etsyController.m_driver.Quit();

           /* var etsyController = createEtsyController();

            string etsyUserLocation = null;
            try
            {
                etsyUserLocation = etsyController.GetEtsyUserLocation();
            }
            catch (Exception exception)
            {
                Globals.HandleException(exception, "Ошибка при получении местоположения");
            }

            string ipLocation = etsyController.IpLocation;
            string ip = etsyController.Ip;

            etsyController.m_driver.Quit();

            MessageBox.Show(string.Format("Местоположение пользователя определённое на etsty.com: {0}.\r\nМестоположение определённое через сторонние сервисы: {1}.\r\nIP: {2}",
                                          etsyUserLocation == null ? "не удалось определить местоположение на сайте Etsy" : etsyUserLocation,
                                          ipLocation, ip),
                            "Результат определения местоположения");*/
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
            try
            {
                string myIP = GetCurrentIP();

                if (m_listIPs.Contains(myIP) &&
                    MessageBox.Show($"Во время работы программы уже добавлялись товары с этого IP {myIP}, может вы забыли поменять его. Продолжить добавление в корзину?",
                                    "Проверка IP", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    return;
                }

                EtsyController etsyController = null;

                var successfullyAddedToCard = 0;
                foreach (var linkInfo in m_list)
                {
                    if (!linkInfo.AddToCard || string.IsNullOrEmpty(linkInfo.Link))
                        continue;

                    if (etsyController == null)
                    {
                        try
                        {
                            etsyController = createEtsyController();
                            etsyController.m_driver.Manage().Window.Maximize();
                        }
                        catch (Exception exception)
                        {
                            throw new Exception("Не удалось создать драйвер для управления хромом.", exception);
                        }
                    }

                    try
                    {
                        etsyController.OpenNewTab(linkInfo.Link);
                    }
                    catch (Exception exception)
                    {
                        SetForeground();
                        Globals.HandleException(exception,
                            $"Не удалось открыть ссылку {linkInfo.Link}");
                        continue;
                    }

                    try
                    {
                        etsyController.PreviewPhotos();

                        etsyController.WatchComments();

                        etsyController.AddCurrentItemToCard();

                        linkInfo.DateLastAdd = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                        ItemsTable.Refresh();

                        ++successfullyAddedToCard;
                    }
                    catch (Exception exception)
                    {
                        SetForeground();
                        Globals.HandleException(exception,
                            $"Возникла ошибка при обработке элемента с номером {m_list.IndexOf(linkInfo) + 1}");
                    }
                }

                SetForeground();

                if (successfullyAddedToCard == 0)
                {
                    MessageBox.Show("Ни одного товара не было добавлено в корзину, возможно у них не стоит галочка добавления или возникли ошибки", "Ошибка добавления товаров в корзину");
                }
                else
                {
                    m_listIPs.Add(myIP);

                    Trace.Assert(etsyController != null, "Добавление в корзину прошло успешно, но контроллер пустой");
                    if (MessageBox.Show($"В корзину успешно добавлено {successfullyAddedToCard} товаров. Закрыть открытые окна?",
                                        "Успех", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        etsyController.m_driver.Quit();
                }
            }
            catch (Exception exception)
            {
                Globals.HandleException(exception, "Возникла ошибка");
            }
        }

        private void ItemsTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == ItemsTable.Columns.IndexOf(OpenLink))
            {
                if (e.RowIndex < m_list.Count)
                {
                    try
                    {
                        Process.Start(m_list[e.RowIndex].Link);
                    }
                    catch (Exception exception)
                    {
                        Globals.HandleException(exception, "Не удалось перейти по ссылке");
                    }
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            m_updateIpThread = new Thread(() =>
            {
                try
                {
                    GetCurrentIP(true);
                    Thread.Sleep(TimeSpan.FromSeconds(20));
                }
                catch (ThreadInterruptedException)
                {
                }
            });
            m_updateIpThread.Start();
        }

        private void CurrentIP_Click(object sender, EventArgs e)
        {
            GetCurrentIP(true);
        }

        private string GetCurrentIP(bool updateTextControl = false)
        {
            string currentIP = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();

            if (updateTextControl)
            {
                CurrentIP.BeginInvoke(new MethodInvoker(() =>
                {
                    CurrentIP.Text = "Текущий IP: " + currentIP;
                }));
            }
            return currentIP;
        }
    }

    public class EtsyLinkInfo
    {
        public bool AddToCard { get; set; } = true;
        public string Link { get; set; }
        public string DateLastAdd { get; set; }
        public string Note { get; set; }
    }
}