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

namespace EtsyPromotion.WebDriver
{
    static class ByAttribute
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

    static class ByLink
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

    abstract class WebDriverHelper
    {
        private string _ip;
        private string _ipLocation;
        private IWebDriver _driver;

        public IWebDriver Driver
        {
            get => _driver ?? (_driver = CreateWebDriver());
            set => _driver = value;
        }

        protected abstract IWebDriver CreateWebDriver();

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

        /// <exception cref="T:OpenQA.Selenium.NoSuchWindowException">If the window cannot be found.</exception>
        public virtual void OpenNewTab(string newUrl)
        {
            if (Driver.Url.Length == 0 || Driver.Url == "data:,")
            {
                Driver.Url = newUrl;
                return;
            }

            var windowsCount = Driver.WindowHandles.Count();
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.open();");

            Trace.Assert(windowsCount < Driver.WindowHandles.Count(), "Не удалось открыть новую вкладку!");

            Driver.SwitchTo().Window(Driver.WindowHandles.Last()); //switches to new tab
            Driver.Navigate().GoToUrl(newUrl);
        }

        /// <exception cref="T:WebDriverException">.</exception>
        public void OpenInNewTab(IWebElement element)
        {
            List<string> previousWindows = Driver.WindowHandles.ToList();

            void SwitchToNewWindow()
            {
                ReadOnlyCollection<string> currentWindows = Driver.WindowHandles;
                foreach (var window in currentWindows)
                {
                    if (previousWindows.FindIndex(v => v == window) == -1)
                    {
                        Driver.SwitchTo().Window(window);
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
            Trace.Assert(Driver.WindowHandles.Count > 0, "Нет вкладок для закрытия");

            bool lastOpenedTab = Driver.WindowHandles.Count <= 1;

            Driver.Close();

            if (!lastOpenedTab)
                Driver.SwitchTo().Window(Driver.WindowHandles.Last()); //switches to last opened tab
        }

        public void Back()
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.history.go(-1);");
        }

        public bool ScrollToElement(IWebElement element)
        {
            Trace.Assert(element != null);
            try
            {
                Actions actions = new Actions(Driver);
                actions.MoveToElement(element);
                actions.Perform();
                return true;
            }
            catch (ArgumentException)
            {
                Debug.Assert(false);
            }
            catch (ElementNotInteractableException)
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
            ((IJavaScriptExecutor)Driver).ExecuteScript($"window.scrollTo({xPosition}, {yPosition})");
        }

        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        public static IWebElement GetParentElement(IWebElement element)
        {
            return element.FindElement(By.XPath("./.."));
        }

        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        private void DetermineConnectionSettings()
        {
            OpenNewTab("https://2ip.ru/");
            try
            {
                IWebElement locationElement = Driver.FindElement(By.ClassName("value-country"));
                _ipLocation = locationElement.Text;

                IWebElement ipElement = Driver.FindElement(By.ClassName("ip"));
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
