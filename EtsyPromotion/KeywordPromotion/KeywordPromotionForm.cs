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

            ListingActionDetails.SetupListingActionsToColumn(ref listingActionColumn, out m_actionDetails);
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
            var smlSerializer = new XmlSerializer(typeof(BindingList<ProductsListItem>));
            using (var wr = new StreamWriter($"promotion_{m_windowIndex}.xml"))
            {
                smlSerializer.Serialize(wr, m_productsList);
            }
        }

        private void LoadSettingsFromXML()
        {
            try
            {
                var smlSerializer = new XmlSerializer(typeof(BindingList<ProductsListItem>));
                using (var rd = new StreamReader($"promotion_{m_windowIndex}.xml"))
                {
                    m_productsList = smlSerializer.Deserialize(rd) as BindingList<ProductsListItem>;
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
                string buttonStartPromotionText = null;

                BeginInvoke(new MethodInvoker(() =>
                {
                    PromotionList.Enabled = false;
                    buttonStartPromotionText = Button_StartPromotion.Text;
                    Button_StartPromotion.Text = "Прервать продвижение";
                }));

                void OnEndExecution()
                {
                    BeginInvoke(new MethodInvoker(() =>
                    {
                        PromotionList.Enabled = true;
                        Button_StartPromotion.Text = buttonStartPromotionText ?? "Запустить продвижение";
                    }));
                }

                PromotionExecutor promotion = null;

                try
                {
                    promotion = new PromotionExecutor(m_productsList);

                    promotion.Execute();
                }
                catch (ThreadInterruptedException)
                {
                    promotion?.ClearErrorMessage();
                }
                catch (Exception)
                {
                    Debug.Assert(false);
                }
                finally
                {
                    OnEndExecution();
                }
            });
            m_workerThread.Start();
        }

        // index of window settings
        private int m_windowIndex;
        private BindingList<ProductsListItem> m_productsList = new BindingList<ProductsListItem>();
        private BindingList<ListingActionDetails> m_actionDetails;
        private Thread m_workerThread;
        private Action m_onFormClosedCallBack;
    }

    public class ProductsListItem
    {
        public ListingActionDetails.ListingAction ItemAction { get; set; }
        public string Link { get; set; }
        public string KeyWords { get; set; }
        public string DateLastAdd { get; set; }
        public string Note { get; set; }
    }
}