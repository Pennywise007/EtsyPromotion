using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace ShopPromotion.Controller
{
    static class ByAttribute
    {
        public static By Name(string attributeName, string tag = "*")
        {
            return By.XPath($".//{tag}[@{attributeName}]");
        }

        public static By Value(string attributeName, string attributeValue, string tag = "*")
        {
            return By.XPath($".//{tag}[@{attributeName} = '{attributeValue}']");
        }

        public static By PartOfValue(string attributeName, string partOfAttributeValue, string tag = "*")
        {
            return By.XPath($".//{tag}[contains(@{attributeName}, '{partOfAttributeValue}')]");
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

    abstract class WebDriverHelper : IBrowserController
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
        public string GetIpLocation()
        {
            if (string.IsNullOrEmpty(_ipLocation))
                DetermineConnectionSettings();
            return _ipLocation;
        }

        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        public string GetIp()
        {
            if (string.IsNullOrEmpty(_ip))
                DetermineConnectionSettings();
            return _ip;
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

        public void OpenInCurrentWindow(string newUrl)
        {
            Driver.Navigate().GoToUrl(newUrl);
        }

        public void Quit()
        {
            Driver.Quit();
        }
        
        public void MaximizeWindow()
        {
            Driver.Manage().Window.Maximize();
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

        public void ScrollToThePageEnd()
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
        }
        
        protected long GetPageHeight()
        {
            return (long)((IJavaScriptExecutor)Driver).ExecuteScript("return document.body.scrollHeight");
        }

        public void ScrollUntilReachThePageEnd()
        {
            var lastHeight = GetPageHeight();

            for (int i = 0; i < 10; i++)
            {
                ScrollToThePageEnd();

                Thread.Sleep(2000);

                var newHeight = GetPageHeight();
                if (newHeight == lastHeight)
                    break;
                else
                    lastHeight = newHeight;
            }
        }

        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        private void DetermineConnectionSettings()
        {
            OpenNewTab("https://2ip.ru/");
            try
            {
                IWebElement locationElement = Driver.FindElement(By.ClassName("value-country"));
                _ipLocation = locationElement.Text.Split('\r').First();

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

        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        public static IWebElement GetParentElement(IWebElement element)
        {
            return element.FindElement(By.XPath("./.."));
        }

        public IWebElement ClickClickableChild(IWebElement element)
        {
            // get all childs
            var childElements = element.FindElements(By.XPath(".//*"));

            foreach (var childElement in childElements)
            {
                try
                {
                    // Проверяем, кликабельный ли элемент
                    if (childElement.Displayed && childElement.Enabled)
                    {
                        childElement.Click();
                        return childElement;
                    }
                }
                catch
                {
                }
            }

            throw new ElementNotInteractableException();
        }

        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        public IWebElement WaitForElement(By by, TimeSpan span, ISearchContext context = null)
        {
            if (context == null)
                context = Driver;

            try
            {
                var wait = new WebDriverWait(Driver, span);
                IWebElement element = wait.Until(driver =>
                {
                    var elements = context.FindElements(by);
                    return elements.FirstOrDefault();
                });
                return element;
            }
            catch (WebDriverTimeoutException e)
            {
                throw new NoSuchElementException($"Element not found within {span.TotalMilliseconds} milliseconds", e);
            }
        }

        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        public List<IWebElement> WaitForElements(By by, TimeSpan span, ISearchContext context = null)
        {
            if (context == null)
                context = Driver;

            try
            {
                var wait = new WebDriverWait(Driver, span);
                var elements = wait.Until(driver =>
                {
                    var foundElements = context.FindElements(by);
                    return foundElements.Any() ? foundElements.ToList() : null;
                });
                return elements;
            }
            catch (WebDriverTimeoutException e)
            {
                throw new NoSuchElementException($"Element not found within {span.TotalMilliseconds} milliseconds", e);
            }
        }

        // Debug function
        public static List<IWebElement> FindCollection(ISearchContext context, string name)
        {
            IReadOnlyCollection<IWebElement> classSelector = context.FindElements(By.ClassName(name));
            IReadOnlyCollection<IWebElement> cssSelector = context.FindElements(By.CssSelector(name));
            IReadOnlyCollection<IWebElement> attributeCSSSelector = context.FindElements(ByAttribute.Name(name));
            IReadOnlyCollection<IWebElement> IdSelector = context.FindElements(By.Id(name));
            IReadOnlyCollection<IWebElement> nameSelector = context.FindElements(By.Name(name));
            IReadOnlyCollection<IWebElement> tagSelector = context.FindElements(By.TagName(name));
            IReadOnlyCollection<IWebElement> xPathSelector = context.FindElements(By.XPath(name));
            IReadOnlyCollection<IWebElement> linkSelector = context.FindElements(By.LinkText(name));
            IReadOnlyCollection<IWebElement> particularLinkSelector = context.FindElements(By.PartialLinkText(name));

            int countNotEmpty = (classSelector.Count() != 0 ? 1 : 0) +
                                (cssSelector.Count() != 0 ? 1 : 0) +
                                (attributeCSSSelector.Count() != 0 ? 1 : 0) +
                                (IdSelector.Count() != 0 ? 1 : 0) +
                                (nameSelector.Count() != 0 ? 1 : 0) +
                                (tagSelector.Count() != 0 ? 1 : 0) +
                                (xPathSelector.Count() != 0 ? 1 : 0) +
                                (linkSelector.Count() != 0 ? 1 : 0) +
                                (particularLinkSelector.Count() != 0 ? 1 : 0);

            Trace.Assert(countNotEmpty <= 1);

            if (classSelector.Count != 0)
                return classSelector.ToList();

            if (cssSelector.Count != 0)
                return cssSelector.ToList();

            if (attributeCSSSelector.Count != 0)
                return attributeCSSSelector.ToList();

            if (IdSelector.Count != 0)
                return IdSelector.ToList();

            if (nameSelector.Count != 0)
                return nameSelector.ToList();

            if (tagSelector.Count != 0)
                return tagSelector.ToList();

            if (xPathSelector.Count != 0)
                return xPathSelector.ToList();

            if (linkSelector.Count != 0)
                return linkSelector.ToList();

            if (particularLinkSelector.Count != 0)
                return particularLinkSelector.ToList();

            return new List<IWebElement>();
        }
    }
}
