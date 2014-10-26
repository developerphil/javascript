using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Selenium.WebDriver.Support.Extensions
{
    public class ScreenShotRemoteWebDriver : RemoteWebDriver, ITakesScreenshot
    {
        private readonly WebDriverEndpoint _endpoint;

        public ScreenShotRemoteWebDriver(WebDriverEndpoint endpoint, ICapabilities capabilities)
            : base(endpoint.RemoteHostUri(), capabilities)
        {
            _endpoint = endpoint;
        }

        public Screenshot GetScreenshot()
        {
            return new Screenshot(Execute(DriverCommand.Screenshot, null).Value.ToString());
        }

        public SessionId GetSessionId()
        {
            return SessionId;
        }

        public string GetNodeHost()
        {
            var request = (HttpWebRequest)WebRequest.Create(_endpoint.TestSessionUrl(SessionId));

            request.Method = "POST";
            request.ContentType = "application/json";

            using (var httpResponse = (HttpWebResponse)request.GetResponse())
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var response = JObject.Parse(streamReader.ReadToEnd());
                return response.SelectToken("proxyId").ToString();
            }
        }
    }
}
