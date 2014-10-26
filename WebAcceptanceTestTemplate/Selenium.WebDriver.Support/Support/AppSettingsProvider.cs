using System;
using System.Configuration;

namespace Selenium.WebDriver.Support.Support
{
    public class AppSettingsProvider
    {
        public T Get<T>(string key) where T : IConvertible
        {
            return Get(key, default(T));
        }

        public T Get<T>(string key, T defaultValue) where T : IConvertible
        {
            var appSetting = ConfigurationManager.AppSettings[key];

            if (appSetting == null)
            {
                return defaultValue;
            }

            return (T)Convert.ChangeType(appSetting, typeof(T));
        }
    }

}
