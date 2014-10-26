using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium.WebDriver.Support.Extensions
{
    public static class WebDriverWaitExtensions
    {
        public static void WaitForElementDisplayed(this IWebDriver driver, By by, TimeSpan timeout)
        {
            var wait = new WebDriverWait(driver, timeout)
            {
                Message = string.Format("Waiting for '{0}' to be displayed", @by)
            };
            wait.Until(drv => drv.FindElement(@by).Displayed);
        }

        public static void WaitForElementHidden(this IWebDriver driver, By by, TimeSpan timeout)
        {
            var wait = new WebDriverWait(driver, timeout)
            {
                Message = string.Format("Waiting for '{0}' to be hidden", @by)
            };
            wait.Until(drv => ElementNotDisplayed(@driver, @by));

        }

        private static bool ElementNotDisplayed(ISearchContext driver, By by)
        {
            try
            {
                return !driver.FindElement(by).Displayed;
            }
            catch (NoSuchElementException)
            {
                return true;
            }
        }

        public static void WaitUntilElementContainsText(this IWebDriver driver, By by, string expectedText,
                                                        TimeSpan timeout)
        {
            var wait = new WebDriverWait(driver, timeout)
            {
                Message = string.Format("Waiting for '{0}' to contain '{1}'", by, expectedText)
            };
            wait.Until(drv => drv.FindElement(@by).Text.Contains(expectedText));
        }
    }
}
