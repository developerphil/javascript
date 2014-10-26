using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using Selenium.WebDriver.Support.Extensions;
using Selenium.WebDriver.Support.Support;
using TechTalk.SpecFlow;

namespace Acceptance.Tests.Template.Reporting
{
    public class GatherData
    {
        private const string ReportPageFormat = "<!DOCTYPE html> \n <html> \n <body> \n <h1>{0}</h1><a href=\"./index.html\">Back</a> \n  {1} \n <a href=\"./index.html\">Back</a></body> \n </html>";

        private const string WindowInformationFormat = "<h4> {0} </h4>\n <table border=1> {1} </table>";
        public static readonly string FilePath = new AppSettingsProvider().Get<string>("TestResultsFolder") + "reports\\";

        private static string GetScenarioTitle()
        {
            return RemoveDiacritics(ScenarioContext.Current.ScenarioInfo.Title);
        }

        private static string RemoveDiacritics(string title)
        {
            var normalized = title.Normalize(NormalizationForm.FormD);
            var resultBuilder = new StringBuilder();
            foreach (var character in normalized)
            {
                var category = CharUnicodeInfo.GetUnicodeCategory(character);
                if (category == UnicodeCategory.LowercaseLetter
                    || category == UnicodeCategory.UppercaseLetter
                    || category == UnicodeCategory.SpaceSeparator)
                    resultBuilder.Append(character);
            }
            return resultBuilder.ToString();
        }

        public static void MoreInfo(IWebDriver driver)
        {
            var originalWindow = driver.CurrentWindowHandle;
            var extendedInformation = string.Format(WindowInformationFormat,
                                                       "Current Window",
                                                       GetAdditionalInformation(driver, 0));
            var windowNumber = 1;
            foreach (var windowHandle in driver.WindowHandles.Where(windowHandle => windowHandle != originalWindow))
            {
                driver.SwitchTo().Window(windowHandle);
                extendedInformation += string.Format(WindowInformationFormat,
                                                     "Window " + windowNumber,
                                                     GetAdditionalInformation(driver, windowNumber));
                windowNumber++;
            }

            extendedInformation = string.Format(ReportPageFormat, GetScenarioTitle(), extendedInformation);

            driver.SwitchTo().Window(originalWindow);

            var extendedInfoFileName = GetScenarioTitle() + ".html";
            File.WriteAllText(FilePath + extendedInfoFileName, extendedInformation);
        }

        private static string GetAdditionalInformation(IWebDriver driver, int windowNumber)
        {
            var screenShotDriver = driver as ScreenShotRemoteWebDriver;

            var info = "";
            const string fileNameFormat = "{0}-{1}.{2}";

            Directory.CreateDirectory(FilePath);
            Directory.CreateDirectory(FilePath + GetScenarioTitle());
            const string tableRow = "<tr><td> {0} </td><td> {1} </td></tr>\n";
            info += string.Format(tableRow, "Error", ScenarioContext.Current.TestError.Message.Replace("\n", "<br>"));

            if (screenShotDriver != null)
            {
                info += string.Format(tableRow, "Grid Node", screenShotDriver.GetNodeHost());
                info += string.Format(tableRow, "Browser Version", screenShotDriver.Capabilities.BrowserName + ": " + screenShotDriver.Capabilities.Version);
            }

            var windowSize = "Height: " + driver.Manage().Window.Size.Height + " Width: " + driver.Manage().Window.Size.Width;
            info += string.Format(tableRow, "Window Size", windowSize);
            info += string.Format(tableRow, "Page url", driver.Url);
            info += string.Format(tableRow, "Page title", driver.Title);
            info += string.Format(tableRow, "Cookies:", GetCookies(driver));

            var fileName = string.Format(fileNameFormat, GetScenarioTitle(), "screenshotWindow" + windowNumber, "png");
            if (screenShotDriver != null)
            {

                screenShotDriver.GetScreenshot().SaveAsFile(FilePath + GetScenarioTitle() + "\\" + fileName, ImageFormat.Png);
                info += string.Format(tableRow, "Screenshot", "<img src=\"./" + GetScenarioTitle() + "/" + fileName + "\">");
            }

            fileName = string.Format(fileNameFormat, GetScenarioTitle(), "pageSourceWindow" + windowNumber, "txt");
            File.WriteAllText(FilePath + GetScenarioTitle() + "\\" + fileName, driver.PageSource);
            info += string.Format(tableRow, "Page Source", "<a href=\"./" + GetScenarioTitle() + "/" + fileName + "\" target=\"_blank\">Page Source</a>");

            return info;
        }

        private static string GetCookies(IWebDriver driver)
        {
            const string cookiesTableFormat = "<table border=1> <tr> <td>Domain</td><td>Path</td><td>Name</td><td>Value</td><td>Expires</td></tr> {0} </table>";
            const string cookieRowFormat = "<tr>" +
                                           "<td>'{0}'</td>" +
                                           "<td>'{1}'</td>" +
                                           "<td>'{2}'</td>" +
                                           "<td>'{3}'</td>" +
                                           "<td>'{4}'</td>" +
                                           "</tr>\n";

            var cookies = driver.Manage().Cookies.AllCookies.Aggregate("", (current, cookie) => current + string.Format(cookieRowFormat, cookie.Domain, cookie.Path, cookie.Name, cookie.Value, cookie.Expiry));

            return string.Format(cookiesTableFormat, cookies);
        }
    }
}
