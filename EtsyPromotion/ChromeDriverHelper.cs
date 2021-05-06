using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace EtsyPromotion
{
    class ByAttribute
    {
        public static By Name(string attributeName, string tag = "*")
        {
            return By.XPath($"//{tag}[@{attributeName}]");
        }

        public static By Value(string attributeName, string attributeValue, string tag = "*")
        {
            return By.XPath($"//{tag}[@{attributeName} = '{attributeValue}']");
        }

        public static By PartOfValue(string attributeName, string partOfAttributeValue, string tag = "*")
        {
            return By.XPath($"//{tag}[contains(@{attributeName}, '{partOfAttributeValue}')]");
        }

        /// <exception cref="T:OpenQA.Selenium.StaleElementReferenceException">Thrown when the target element is no longer valid in the document DOM.</exception>
        public static bool IsAttributeExist(IWebElement elements, string attributeName)
        {
            return elements.GetAttribute(attributeName) != null;
        }
    }

    class ByLink
    {
        public static string GetElementLink(IWebElement elements)
        {
            const string linkAttributeName = "href";
            string elementLink = null;
            try
            {
                elementLink = elements.GetAttribute(linkAttributeName);
            }
            catch (StaleElementReferenceException) { }

            if (string.IsNullOrEmpty(elementLink))
            {
                try
                {
                    var elementWithLink = elements.FindElement(By.XPath(".//a"));
                    elementLink = elementWithLink.GetAttribute(linkAttributeName) ?? elementWithLink.GetAttribute("data-href");
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }
            }
            return elementLink;
        }
    }
    class ChromeDriverHelper
    {
        private string _ip;
        private string _ipLocation;

        public IWebDriver m_driver;

        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        public string IpLocation
        {
            get
            {
                if (string.IsNullOrEmpty(_ipLocation))
                    DetermineConnectionSettings();
                return _ipLocation;
            }
        }

        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        public string Ip
        {
            get
            {
                if (string.IsNullOrEmpty(_ip))
                    DetermineConnectionSettings();
                return _ip;
            }
        }

        public ChromeDriverHelper()
        {
            // start new chrome as incognito
            var options = new ChromeOptions();
            //options.AddArguments("--incognito");

            m_driver = new ChromeDriver(options);
        }

        /// <exception cref="T:OpenQA.Selenium.NoSuchWindowException">If the window cannot be found.</exception>
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

        /// <exception cref="T:WebDriverException">.</exception>
        public void OpenInNewTab(IWebElement element)
        {
            List<string> previousWindows = m_driver.WindowHandles.ToList();

            void SwitchToNewWindow()
            {
                ReadOnlyCollection<string> currentWindows = m_driver.WindowHandles;
                foreach (var window in currentWindows)
                {
                    if (previousWindows.FindIndex(v => v == window) == -1)
                    {
                        m_driver.SwitchTo().Window(window);
                        return;
                    }
                }
            }

            try
            {
                element.SendKeys(Keys.Control + Keys.Shift + Keys.Enter);
                SwitchToNewWindow();
            }
            catch (WebDriverException)
            {
                try
                {
                    var linkElement = element.FindElement(By.TagName("a"));
                    linkElement.SendKeys(Keys.Control + Keys.Shift + Keys.Enter);
                    SwitchToNewWindow();

                    return;
                }
                catch (WebDriverException)
                {
                }

                throw;
            }
        }

        /// <exception cref="T:OpenQA.Selenium.NoSuchWindowException">If the window cannot be found.</exception>
        public void CloseCurrentTab()
        {
            Trace.Assert(m_driver.WindowHandles.Count > 0, "Нет вкладок для закрытия");

            m_driver.Close();
            m_driver.SwitchTo().Window(m_driver.WindowHandles.Last()); //switches to last opened tab
        }

        public void Back()
        {
            ((IJavaScriptExecutor)m_driver).ExecuteScript("window.history.go(-1);");
        }

        public bool ScrollToElement(IWebElement element)
        {
            Trace.Assert(element != null);
            try
            {
                Actions actions = new Actions(m_driver);
                actions.MoveToElement(element);
                actions.Perform();
                return true;
            }
            catch (ArgumentException)
            {
                Debug.Assert(false);
            }

            if (element.Location.IsEmpty)
                return false;

            ScrollTo(element.Location.X, element.Location.Y);
            return true;
        }

        public void ScrollTo(int xPosition = 0, int yPosition = 0)
        {
            ((IJavaScriptExecutor)m_driver).ExecuteScript($"window.scrollTo({xPosition}, {yPosition})");
        }

        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        public IWebElement GetParentElement(IWebElement element)
        {
            return element.FindElement(By.XPath("./.."));
        }

        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
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
            catch (StaleElementReferenceException exception)
            {
                throw new NoSuchElementException("Не удалось получить информацию о текущем положении c https://2ip.ru/. \n\n", exception);
            }
            catch (NoSuchElementException exception)
            {
                throw new NoSuchElementException("Не удалось получить информацию о текущем положении c https://2ip.ru/. \n\n", exception);
            }
            finally
            {
                CloseCurrentTab();
            }
        }
    }
}
