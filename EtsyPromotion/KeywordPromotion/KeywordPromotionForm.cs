using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using MetroFramework.Forms;
using OpenQA.Selenium;

namespace EtsyPromotion.KeywordPromotion
{
    public partial class KeywordPromotionForm : MetroForm
    {
        public KeywordPromotionForm(int windowIndex, Action onFormClosed)
        {
            m_windowIndex = windowIndex;
            m_onFormClosedCallBack = onFormClosed;

            InitializeComponent();

            LoadSettingsFromXML();

            PromotionList.DataSource = new BindingSource
            {
                DataSource = m_productsList,
                AllowNew = true
            };
        }

        private void KeywordPromotion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_workerThread != null &&
                MessageBox.Show("Если закрыть окно то продвижение указанных товаров приостановится, продолжить закрытие?",
                                "Закрытие окна",
                                MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;
        }

        private void KeywordPromotion_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_workerThread?.Interrupt();
            m_workerThread?.Join();

            SaveSettingsToXML();

            m_onFormClosedCallBack();
        }

        private void SaveSettingsToXML()
        {
            var smlSerializer = new XmlSerializer(typeof(List<ProductsListItem>));
            using (var wr = new StreamWriter($"promotion_{m_windowIndex}.xml"))
            {
                smlSerializer.Serialize(wr, m_productsList);
            }
        }

        private void LoadSettingsFromXML()
        {
            try
            {
                var smlSerializer = new XmlSerializer(typeof(List<ProductsListItem>));
                using (var rd = new StreamReader($"promotion_{m_windowIndex}.xml"))
                {
                    m_productsList = smlSerializer.Deserialize(rd) as List<ProductsListItem>;
                }
            }
            catch (FileNotFoundException)
            {
            }
            catch (Exception exception)
            {
                EtsyPromotion.Globals.HandleException(exception, "Ошибка при загрузке настроек");
            }
        }

        private void Button_StartPromotion_Click(object sender, EventArgs e)
        {
            if (m_workerThread != null)
            {
                m_workerThread.Interrupt();
                m_workerThread.Join();
                return;
            }

            m_workerThread = new Thread(() =>
            {
                string errorMessage = null;
                string buttonStartPromotionText = null;

                BeginInvoke(new MethodInvoker(() =>
                {
                    PromotionList.Enabled = false;
                    buttonStartPromotionText = Button_StartPromotion.Text;
                    Button_StartPromotion.Text = "Прервать продвижение";
                }));

                SearchController controller = null;

                void OnEndExecution()
                {
                    BeginInvoke(new MethodInvoker(() =>
                    {
                        PromotionList.Enabled = true;
                        Button_StartPromotion.Text = buttonStartPromotionText ?? "Запустить продвижение";
                    }));

                    controller?.m_driver.Quit();

                    if (!string.IsNullOrEmpty(errorMessage))
                        MessageBox.Show(errorMessage, "При выполнении продвижения возникли ошибки.");
                }

                try
                {
                    void AddErrorMessage(string message)
                    {
                        errorMessage = message + "\n";
                    }

                    List<PromotionInfo> productsList = new List<PromotionInfo>();

                    // valisdate and transform all parameters
                    foreach (var productInfo in m_productsList)
                    {
                        if (!productInfo.Enable)
                            continue;

                        bool failed = false;
                        var keyWordsArray = productInfo.KeyWords.Split(';');
                        if (!keyWordsArray.Any())
                        {
                            failed = true;
                            AddErrorMessage(
                                $"Не удалось получить список ключевых слов из '{productInfo.KeyWords}', проверьте формат у элемента '{productInfo.KeyWords}'.");
                        }

                        string listingID = null;
                        try
                        {
                            // link example https://www.etsy.com/listing/ID/... try extract ID
                            var match = Regex.Match(productInfo.Link, "etsy.com/listing/(.*)/");
                            listingID = match.Groups[1].Value;
                        }
                        catch (Exception exception)
                        {
                            failed = true;
                            AddErrorMessage(
                                $"Не удалось получить идентификатор из ссылки '{productInfo.Link}', проверьте что ссылка у элемента '{productInfo.KeyWords} валидна\n\n" +
                                exception.ToString());
                        }

                        if (!failed)
                            productsList.Add(new PromotionInfo
                                {m_listingId = listingID, m_keyWords = keyWordsArray.ToList()});
                    }

                    if (!productsList.Any())
                    {
                        return;
                    }

                    controller = new SearchController();
                    controller.m_driver.Manage().Window.Maximize();

                    controller.OpenNewTab("https://www.etsy.com/");

                    foreach (var productInfo in productsList)
                    {
                        foreach (var keyWord in productInfo.m_keyWords)
                        {
                            controller.SearchText(keyWord);

                            int currentPage = 0;
                            IWebElement productLink = null;

                            do
                            {
                                if (currentPage != 0)
                                {
                                    // whaiting for loading page
                                    Thread.Sleep(3000);
                                }

                                ++currentPage;

                                List<IWebElement> allResults = new List<IWebElement>();
                                try
                                {
                                    allResults = controller.getListOfSearchResults();
                                }
                                catch (NoSuchElementException)
                                {
                                }

                                void ScrollResults()
                                {
                                    int? stopIndex = null;

                                    if (productLink != null)
                                    {
                                        try
                                        {
                                            stopIndex = allResults.FindIndex(element =>
                                                element.TagName == productLink.TagName);
                                        }
                                        catch (ArgumentNullException exception)
                                        {
                                            Debug.Assert(false);
                                        }
                                    }

                                    for (int currentIndex = 0, maxIndex = stopIndex ?? allResults.Count;
                                        currentIndex < maxIndex;
                                        currentIndex += 7)
                                    {
                                        if (controller.ScrollToElement(allResults[currentIndex]))
                                            Thread.Sleep(1000);
                                    }

                                    if (stopIndex == null)
                                        return;

                                    if (controller.ScrollToElement(allResults[stopIndex.Value]))
                                        Thread.Sleep(1000);
                                }

                                try
                                {
                                    productLink = controller.FindListingInSearchResults(productInfo.m_listingId);
                                }
                                catch (NoSuchElementException)
                                {
                                }

                                ScrollResults();
                            } while (productLink == null && currentPage < 100 && controller.OpenNextSearchPage());

                            if (productLink != null)
                            {
                                try
                                {
                                    productLink.Click();

                                    controller.PreviewPhotos();

                                    controller.WatchComments();

                                    controller.AddCurrentItemToCard();
                                }
                                catch (Exception exception)
                                {
                                    AddErrorMessage(
                                        $"Не удалось добавить товар с идентификатором {productInfo.m_listingId} в корзину.");
                                }
                            }
                            else
                            {
                                if (currentPage < 2)
                                {
                                    AddErrorMessage(
                                        $"Не удалось найти товар c {productInfo.m_listingId} по ключевому слову {keyWord}.\n" +
                                        "Проверьте что по ключевым словам есть результаты поиска. " +
                                        "Если есть много страниц с результатами значит программа не смогла переключить страницы, обратитесь к автору.");
                                }
                                else
                                {
                                    AddErrorMessage(
                                        $"Не удалось найти товар c идентификатором {productInfo.m_listingId} по ключевому слову {keyWord}.");
                                }
                            }
                        }
                    }
                }
                catch (ThreadInterruptedException)
                {
                    errorMessage = null;
                }
                catch (Exception)
                {
                }
                finally
                {
                    OnEndExecution();
                }
            });
            m_workerThread.Start();
        }

        public class PromotionInfo
        {
            public string m_listingId;
            public List<string> m_keyWords;
        }

        // index of window settings
        private int m_windowIndex;
        private List<ProductsListItem> m_productsList = new List<ProductsListItem>();
        private Thread m_workerThread;
        private Action m_onFormClosedCallBack;
    }

    public class ProductsListItem
    {
        public bool Enable { get; set; } = true;
        public string Link { get; set; }
        public string KeyWords { get; set; }
        public string DateLastAdd { get; set; }
        public string Note { get; set; }
    }
}