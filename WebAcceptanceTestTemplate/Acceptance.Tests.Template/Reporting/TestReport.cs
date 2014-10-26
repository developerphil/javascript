using System.Collections.Generic;
using System.IO;
using System.Linq;
using Selenium.WebDriver.Support.Support;

namespace Acceptance.Tests.Template.Reporting
{
    public class TestReport
    {
        public static List<string> ErrorList = new List<string>(); 

        public static void GenerateTestReport()
        {
            var appSettingsProvider = new AppSettingsProvider();
            var filePath = appSettingsProvider.Get<string>("TestResultsFolder");

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            const string indexFormat = "<!DOCTYPE html> \n <html> \n <body> \n <h1>Failed Scenarios</h1> \n {0} \n </body> \n </html>";

            var scenarioFiles = new List<string>();

            if (Directory.Exists(filePath + "reports\\"))
            {
                scenarioFiles = Directory.GetFiles(filePath + "reports\\", "*.html").ToList();
            }

            string indexList;
            if (scenarioFiles.Count > 0)
            {
                const string failedScenarioLinkFormat = "<a href=\"./{0}\">{1}</a><br>\n";
                indexList = scenarioFiles.Select(scenarioFile => scenarioFile.Replace(filePath, "")).Aggregate("", (current, scenarioFileName) => current + string.Format(failedScenarioLinkFormat, scenarioFileName, scenarioFileName.Replace(".html", "")));
            }
            else
            {
                indexList = "<p>All Scenarios Passed</p>";
            }

            File.WriteAllText(filePath + "report.html", string.Format(indexFormat, indexList));
        }
    }
}
