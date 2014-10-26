using System;
using System.Threading;
using Acceptance.Tests.Template.Reporting;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Selenium.WebDriver.Support;
using TechTalk.SpecFlow;

namespace Acceptance.Tests.Template.Steps
{
    [Binding]
    public class CommonSteps : BaseSteps
    {
        private static WebDriverGrid _webDriverGrid;

        [BeforeTestRun]
        public static void RunSetup()
        {
            var webDriverEndpoint = WebDriverEndpoint.Load();
            _webDriverGrid = new WebDriverGrid(webDriverEndpoint);

            if (webDriverEndpoint.IsLocal())
            {
                _webDriverGrid.Start();
                Thread.Sleep(2000);
            }
        }

        [AfterTestRun]
        public static void RunTearDown()
        {
            _webDriverGrid.Dispose();
            TestReport.GenerateTestReport();
        }

        [BeforeScenario]
        public void SetUp()
        {
        }

        [AfterScenario]
        public void TearDown()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                TestReport.ErrorList.Add(ScenarioContext.Current.ScenarioInfo.Title);
                TemplateBrowser.GenerateErrorReport();
            }
        }

        [AfterFeature]
        public static void FeatureTearDown()
        {
            TemplateBrowser.Dispose();
        }

        [Given(@"I have no cookies set")]
        public void GivenIHaveNoCookiesSet()
        {
            TemplateBrowser.ClearCookies();
        }

        [When(@"I navigate to the (.*) page")]
        public void NavigateToPage(string page)
        {
            TemplateBrowser.NavigateToUrl(String.Format("{0}{1}", TemplateBrowser.BaseUrl, page));
        }

        [When(@"I click on the ""(.*)"" element")]
        public void ClickElement(string elementId)
        {
            TemplateBrowser.Click(elementId);
        }

        [When(@"I click the ""(.*)"" link")]
        public void ClickLink(string link)
        {
            var linkElement = TemplateBrowser.GetElementByXPath(string.Format("//a[text()='{0}']", link));
            Assert.That(linkElement, Is.Not.Null, string.Format("Link with text {0} not found on page",link));
            linkElement.Click();
        }

        [When(@"I change the (.*) dropdown to (.*)")]
        public void SelectDropDownText(string elementId, string text)
        {
            TemplateBrowser.SelectDropDownItem(elementId, text);
        }

        [When(@"I set the (.*) cookie to (.*)")]
        public void SetCookie(string cookieName, string cookieValue)
        {
            TemplateBrowser.SetCookie(cookieName, cookieValue);
        }

        [When(@"I wait (.*) seconds")]
        public void Wait(int wait)
        {
            TemplateBrowser.Wait(wait);
        }

        [When(@"I navigate to the url (.*)")]
        public void NavigateToUrl(string url)
        {
            TemplateBrowser.NavigateToUrl(url);
        }

        [Then(@"the metadata page title should contain the text (.*)")]
        public void ThenTheMetadataPageTitleShouldContain(string text)
        {
            Assert.That(TemplateBrowser.PageTitle, Is.StringContaining(text));
        }

        [Then(@"the title should contain (.*)")]
        public void ThenTheTitleShouldContainSpecflow(string text)
        {
            Assert.That(TemplateBrowser.PageTitle, Is.StringContaining(text));
        }

        [Then(@"the (.*) text should be (.*)")]
        public void ElementTextShouldBe(string id, string value)
        {
            Assert.That(TemplateBrowser.GetElementText(id), Is.EqualTo(value));
        }

        public static void TestHref(IWebElement link, string expectedHref)
        {
            var isRelative = string.IsNullOrEmpty(expectedHref) || expectedHref.StartsWith("/");

            if (isRelative)
                link.GetAttribute("href").Should().EndWith(expectedHref);
            else
                link.GetAttribute("href").Should().StartWith(expectedHref);
        }
    }

}
