using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace EtsyPromotion
{
    class AttributeHelper
    {
        public static By SelectorByAttribute(string attributeName, string tag = "*")
        {
            return By.XPath($"//{tag}[@{attributeName}]");
        }

        public static By SelectorByAttributeValue(string attributeName, string attributeValue, string tag = "*")
        {
            return By.XPath($"//{tag}[@{attributeName} = '{attributeValue}']");
        }

        public static bool IsAttributeExist(IWebElement elements, string attributeName)
        {
            return elements.GetAttribute(attributeName) != null;
        }
    }

    class ChromeDriverHelper
    {
        private string _ip;
        private string _ipLocation;

        public IWebDriver m_driver;

        public string IpLocation
        {
            get
            {
                if (string.IsNullOrEmpty(_ipLocation))
                    DetermineConnectionSettings();
                return _ipLocation;
            }
        }

        public string Ip
        {
            get
            {
                if (string.IsNullOrEmpty(_ip))
                    DetermineConnectionSettings();
                return _ip;
            }
        }

        public ChromeDriverHelper(IWebDriver driver)
        {
            m_driver = driver;
        }

        public void OpenNewTab(string newUrl)
        {
            if (m_driver.Url.Length == 0 || m_driver.Url == "data:,")
            {
                m_driver.Url = newUrl;
                return;
            }

            var windowsCount = m_driver.WindowHandles.Count();
            ((IJavaScriptExecutor)m_driver).ExecuteScript("window.open();");

            Trace.Assert(windowsCount < m_driver.WindowHandles.Count(), "Не удалось открыть новую вкладку!");

            m_driver.SwitchTo().Window(m_driver.WindowHandles.Last()); //switches to new tab
            m_driver.Navigate().GoToUrl(newUrl);
        }

        public void OpenInNewTab(IWebElement element)
        {
            //Actions action = new Actions(m_driver);
            //action.SendKeys(element, Keys.Control + Keys.Shift + Keys.Enter);

            element.SendKeys(Keys.Control + Keys.Shift + Keys.Enter);
        }

        public void CloseCurrentTab()
        {
            Trace.Assert(m_driver.WindowHandles.Count > 0, "Нет вкладок для закрытия");

            ((IJavaScriptExecutor)m_driver).ExecuteScript("window.close();");
            m_driver.SwitchTo().Window(m_driver.WindowHandles.Last()); //switches to last opened tab
        }

        public void Back()
        {
            ((IJavaScriptExecutor)m_driver).ExecuteScript("window.history.go(-1);");
        }

        public void ScrollToElement(IWebElement element)
        {
            try
            {
                Actions actions = new Actions(m_driver);
                actions.MoveToElement(element);
                actions.Perform();
            }
            catch (Exception)
            {
            }
        }
        public void ScrollTo(int xPosition = 0, int yPosition = 0)
        {
            ((IJavaScriptExecutor)m_driver).ExecuteScript($"window.scrollTo({xPosition}, {yPosition})");
        }

        private void DetermineConnectionSettings()
        {
            OpenNewTab("https://2ip.ru/");
            try
            {
                IWebElement locationElement = m_driver.FindElement(By.ClassName("value-country"));
                _ipLocation = locationElement.Text;

                IWebElement ipElement = m_driver.FindElement(By.ClassName("ip"));
                _ip = ipElement.Text;
            }
            catch (Exception exception)
            {
                throw new Exception("Не удалось получить информацию о текущем положении c https://2ip.ru/. \n\n", exception);
            }
            finally
            {
                CloseCurrentTab();
            }
        }

    }
}
