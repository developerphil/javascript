using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.WebDriver.Support.Extensions;

namespace Selenium.WebDriver.Support
{
    public class WebDriverBuilder
    {
        private readonly DesiredCapabilities _capabilities = DesiredCapabilities.Chrome();
        private WebDriverEndpoint _endpoint;

        public IWebDriver Build()
        {
            return new ScreenShotRemoteWebDriver(_endpoint, _capabilities);
        }

        public WebDriverBuilder WithHub(WebDriverEndpoint endpoint)
        {
            _endpoint = endpoint;
            return this;
        }

        public WebDriverBuilder AllowingUntrustedSslCertificates()
        {
            _capabilities.SetCapability(CapabilityType.AcceptSslCertificates, true);

            return this;
        }

        public WebDriverBuilder WithProxy(string proxy, string exceptions)
        {
            _capabilities.SetCapability(CapabilityType.Proxy, new Proxy
            {
                HttpProxy = proxy,
                FtpProxy = proxy,
                SslProxy = proxy,
                NoProxy = ""
            });

            return this;
        }
    }
}
