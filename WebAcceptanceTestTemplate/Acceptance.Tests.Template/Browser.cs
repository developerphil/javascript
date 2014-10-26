using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Acceptance.Tests.Template.Reporting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Selenium.WebDriver.Support;
using Selenium.WebDriver.Support.Extensions;
using Selenium.WebDriver.Support.Support;

namespace Acceptance.Tests.Template
{
    public sealed class Browser : IDisposable
    {
        private readonly IWebDriver _browser;
        public string BaseUrl { get; private set; }

        public Browser()
        {
            _browser = new WebDriverBuilder()
                .AllowingUntrustedSslCertificates()
                .WithHub(WebDriverEndpoint.Load())
                .Build();

            BaseUrl = new AppSettingsProvider().Get<string>("BaseUrl");
        }

        public void GenerateErrorReport()
        {
            GatherData.MoreInfo(_browser);
        }

        public string CurrentPageUrl
        {
            get { return _browser.Url; }
        }

        public string PageTitle
        {
            get { return _browser.Title; }
        }

        public void ClearCookies()
        {
            _browser.Manage().Cookies.DeleteAllCookies();
        }

        public void NavigateToUrl(string url)
        {
            if (url.StartsWith("/")) url = BaseUrl + url;

            _browser.Navigate().GoToUrl(url);
        }

        public string GetRelativeHref(IWebElement element)
        {
            var href = element.GetAttribute("href");

            if (href == null) throw new Exception("href attribute not found on element");

            var baseUrl = string.Format("http://{0}", BaseUrl);

            return href.Replace(baseUrl, string.Empty);
        }

        private void WaitForElement(By by, TimeSpan timout)
        {
            new WebDriverWait(_browser, timout).Until(r => r.FindElements(by).Any());
        }

        public IWebElement GetElement(string id)
        {
            return _browser.FindElement(By.Id(id));
        }

        public IWebElement GetElementByName(string name)
        {
            return _browser.FindElement(By.Name(name));
        }

        public IWebElement GetElementByLinkText(string linkText)
        {
            return _browser.FindElement(By.LinkText(linkText));
        }

        public IReadOnlyCollection<IWebElement> GetElementsByXPath(string xPath)
        {
            return _browser.FindElements(By.XPath(xPath));
        }

        public IReadOnlyCollection<IWebElement> GetElementsByXPath(string xPath, TimeSpan timeout)
        {
            WaitForElement(By.XPath(xPath), timeout);
            return GetElementsByXPath(xPath);
        }

        public IWebElement GetElementByXPath(string xPath)
        {
            return _browser.FindElement(By.XPath(xPath));
        }

        public IWebElement GetElementByXPath(string xPath, TimeSpan timeout)
        {
            WaitForElement(By.XPath(xPath), timeout);
            return GetElementByXPath(xPath);
        }

        public IWebElement GetElementWithText(string text)
        {
            return GetElementByXPath(String.Format("//*[text()='{0}']", text));
        }

        public IWebElement GetElementByTagName(string tagName)
        {
            return _browser.FindElement(By.TagName(tagName));
        }

        public IEnumerable<IWebElement> GetElementsByTag(string tagName)
        {
            return _browser.FindElements(By.TagName(tagName));
        }

        public string GetElementText(string id)
        {
            return GetElement(id).Text;
        }

        public void SetElementText(string id, string text)
        {
            GetElement(id).SendKeys(text);
        }

        public void Click(string id)
        {
            GetElement(id).Click();
        }

        public void SelectDropDownItem(string id, string text)
        {
            var dropdown = new SelectElement(GetElement(id));
            dropdown.DeselectAll();
            dropdown.SelectByText(text);
        }

        public void WaitForElementToBeDisplayed(By by, TimeSpan timeout)
        {
            _browser.WaitForElementDisplayed(by, timeout);
        }

        public void WaitForElementToBeHidden(By by, TimeSpan timeout)
        {
            _browser.WaitForElementHidden(by, timeout);
        }

        public void WaitUntilElementContainsText(By by, string expectedText, TimeSpan timeout)
        {
            _browser.WaitUntilElementContainsText(by, expectedText, timeout);
        }

        public void Wait(int seconds)
        {
            Thread.Sleep(1000 * seconds);
        }

        public void SetCookie(string name, string value)
        {
            SetCookie(name, value, BaseUrl);
        }

        public void SetCookie(string name, string value, string domain)
        {
            var cookie = new Cookie(name, value, domain, "/", null);

            _browser.Manage().Cookies.AddCookie(cookie);
        }

        public int IndexOfInPageSource(string searchFor)
        {
            return _browser.PageSource.IndexOf(searchFor, StringComparison.Ordinal);
        }

        public void DeleteCookie(string cookieName)
        {
            _browser.Manage().Cookies.DeleteCookieNamed(cookieName);
        }

        public void MouseOver(string linkText)
        {
            var element = GetElementByLinkText(linkText);

            new Actions(_browser).MoveToElement(element).Build().Perform();
        }

        public void Dispose()
        {
            _browser.Close();
            _browser.Dispose();
        }
    }
}
