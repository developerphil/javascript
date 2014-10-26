using System;
using OpenQA.Selenium.Remote;
using Selenium.WebDriver.Support.Support;

namespace Selenium.WebDriver.Support
{
    public class WebDriverEndpoint
    {
        public static string DefaultHost = "localhost";
        public static int DefaultPort = 4444;

        public WebDriverEndpoint(string host, int port)
        {
            Host = host;
            Port = port;
        }

        public string Host { get; private set; }
        public int Port { get; private set; }

        public bool IsLocal()
        {
            return DefaultHost == Host;
        }

        public Uri RemoteHostUri()
        {
            return new Uri(BaseUri() + "wd/hub");
        }

        public Uri TestSessionUrl(SessionId sessionId)
        {
            return new Uri(string.Format("{0}grid/api/testsession?session={1}", BaseUri(), sessionId));
        }

        private Uri BaseUri()
        {
            return new Uri(string.Format("http://{0}:{1}", Host, Port));
        }

        public static WebDriverEndpoint Load()
        {
            var hub = new AppSettingsProvider().Get("webdriver.hub.host", DefaultHost);
            var port = new AppSettingsProvider().Get("webdriver.hub.port", DefaultPort);

            return new WebDriverEndpoint(hub, port);
        }
    }
}
