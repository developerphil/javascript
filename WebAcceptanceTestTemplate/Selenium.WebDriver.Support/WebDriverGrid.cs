using System;
using System.Linq;
using System.Diagnostics;
using System.IO;

namespace Selenium.WebDriver.Support
{
    public class WebDriverGrid : IDisposable
    {
        private Process _gridProcess;
        private Process _nodeProcess;
        private readonly WebDriverEndpoint _endpoint;

        public WebDriverGrid(WebDriverEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        public WebDriverGrid Start()
        {
            StartGrid();
            RegisterNode();
            return this;
        }

        private void StartGrid()
        {
            var args = string.Format("-jar selenium-server-standalone-2.41.0.jar -role hub -port {0}", _endpoint.Port);
            _gridProcess = StartSeleniumProcess(args);
        }

        private void RegisterNode()
        {
            var args = string.Format("-jar selenium-server-standalone-2.41.0.jar -role node -hubHost {0} -hubPort {1}", _endpoint.Host, _endpoint.Port);
            _nodeProcess = StartSeleniumProcess(args);
        }

        private Process StartSeleniumProcess(string args)
        {
            if (ChromeDriverNotInstalled())
            {
                throw new FileNotFoundException("chromedriver.exe not found in PATH, is chrome driver installed? (Restart grid or Restart visual studio after setting path)");
            }

            if (JavaNotInstalled())
            {
                throw new FileNotFoundException("java.exe not found in PATH, is Java installed?");
            }

            var processInfo = new ProcessStartInfo("java.exe", args)
            {
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process;
            if ((process = Process.Start(processInfo)) == null)
            {
                throw new InvalidOperationException("??");
            }

            if (process.HasExited)
            {
                throw new InvalidOperationException("Process has exited check selenium jar file is copied to test run location and version is correct.");
            }

            return process;
        }

        private static bool JavaNotInstalled()
        {
            var values = Environment.GetEnvironmentVariable("PATH");
            return !(values != null && values.Split(';').Select(path => Path.Combine(path, "java.exe")).Any(File.Exists));
        }

        private static bool ChromeDriverNotInstalled()
        {
            var values = Environment.GetEnvironmentVariable("PATH");
            return !(values != null && values.Split(';').Select(path => Path.Combine(path, "chromedriver.exe")).Any(File.Exists));
        }

        private void SetChromeDriverLocation()
        {
            if (ChromeDriverNotInstalled())
            {
                string currentPath = Environment.GetEnvironmentVariable("path");
                Environment.SetEnvironmentVariable("Path", string.Format("{0};{1}\\", currentPath, AppDomain.CurrentDomain.BaseDirectory), EnvironmentVariableTarget.Machine);
            }
        }

        public void Dispose()
        {
            if (_gridProcess != null && !_gridProcess.HasExited)
            {
                _gridProcess.Kill();
            }
            if (_nodeProcess != null && !_nodeProcess.HasExited)
            {
                _nodeProcess.Kill();
            }
        }

        ~WebDriverGrid()
        {
            Dispose();
        }
    }
}
