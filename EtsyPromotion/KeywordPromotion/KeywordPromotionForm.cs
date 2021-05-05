using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using MetroFramework.Forms;

namespace EtsyPromotion.KeywordPromotion
{
    public partial class KeywordPromotionForm : MetroForm
    {
        public KeywordPromotionForm(int windowIndex, Action onFormClosed)
        {
            m_windowIndex = windowIndex;
            m_onFormClosedCallBack = onFormClosed;

            InitializeComponent();

            ListingActionDetails.SetupListingActionsToColumn(ref listingActionColumn);
            LoadSettingsFromXML();

            PromotionList.DataSource = new BindingSource
            {
                DataSource = m_productsList,
                AllowNew = true
            };
        }

        private void KeywordPromotion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_promotionThread != null && m_promotionThread.IsAlive &&
                MessageBox.Show("Если закрыть окно то продвижение указанных товаров приостановится, продолжить закрытие?",
                                "Закрытие окна",
                                MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;
        }

        private void KeywordPromotion_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_promotionThread != null && m_promotionThread.IsAlive)
            {
                try
                {
                    m_promotionThread?.Interrupt();
                    m_promotionThread?.Join();
                }
                catch (Exception exception)
                {
                    Globals.HandleException(exception, "Ошибка прерывания потока");
                }
            }

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
                Globals.HandleException(exception, "Ошибка при загрузке настроек");
            }
        }

        private void Button_StartPromotion_Click(object sender, EventArgs e)
        {
            if (m_promotionThread != null && m_promotionThread.IsAlive)
            {
                Button_StartPromotion.Enabled = false;
                Button_StartPromotion.Text = "Прерываем продвижение...";

                m_promotionThread.Interrupt();
                return;
            }

            m_promotionThread = new Thread(() =>
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
                        Button_StartPromotion.Enabled = true;
                        Button_StartPromotion.Text = buttonStartPromotionText ?? "Запустить продвижение";
                    }));
                }

                try
                {
                    PromotionExecutor promotion = null;

                    try
                    {
                        promotion = new PromotionExecutor(m_productsList, (index) =>
                        {
                            m_productsList[index].DateLastPromotion = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                            BeginInvoke(new MethodInvoker(() => PromotionList.Refresh()));
                        });

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

                    promotion.Dispose();

                    OnEndExecution();
                }
                catch (ThreadInterruptedException)
                {
                }
                catch (Exception exception)
                {
                    Globals.HandleException(exception, "Возникла критическая ошибка во время продвижения.");
                }
            });

            m_promotionThread.Start();
        }

        // index of window settings
        private int m_windowIndex;
        private BindingList<ProductsListItem> m_productsList = new BindingList<ProductsListItem>();
        private Thread m_promotionThread;
        private Action m_onFormClosedCallBack;
    }

    public class ProductsListItem
    {
        public ListingActionDetails.ListingAction ItemAction { get; set; } = ListingActionDetails.ListingAction.AddToCard;
        public string Link { get; set; }
        public string KeyWords { get; set; }
        public string DateLastPromotion { get; set; }
        public string Note { get; set; }
    }
}