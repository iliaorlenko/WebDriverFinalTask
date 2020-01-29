using Allure.Commons;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Html5;
using OpenQA.Selenium.Remote;
using System;

namespace WebDriverFinalTask.Base
{
    public class DriverContext : AllureReport
    {
        protected Environment selectedEnvironment;

        public IWebDriver GetDriver(BrowserName browser)
        {

            IWebDriver Driver = null;

            string GridEnvironment = null;

            FirefoxOptions firefoxOpts = new FirefoxOptions();

            ChromeOptions chromeOpts = new ChromeOptions();

            //chromeOpts.AddArgument("--start-maximized");
            //chromeOpts.AddArgument("--disable-dev-shm-usage");
            //chromeOpts.AddArgument("--disable-gpu");
            //chromeOpts.AddArgument("--no-sandbox");
            chromeOpts.UnhandledPromptBehavior = UnhandledPromptBehavior.Accept;
            //chromeOpts.AddAdditionalCapability("w3c", false);
            chromeOpts.UseSpecCompliantProtocol = false;
            //chromeOpts.AddAdditionalCapability("supportsWebStorage", CapabilityType.SupportsWebStorage);


            // If environment == local, return new driver and finish driver setup
            if (selectedEnvironment == Environment.Local)
            {
                switch (browser)
                {
                    case BrowserName.Firefox:
                        Driver = new FirefoxDriver(firefoxOpts);
                        break;

                    default:
                        Driver = new ChromeDriver(chromeOpts);
                        break;
                }
            }

            else
                if(selectedEnvironment == Environment.BrowserStack)
                {
                // Else if environment == browserstack, switch browser and return new RemoteWebDriver with BrowserStackUri and selected browser capabilities
                chromeOpts.AddGlobalCapability("os", "Windows");
                chromeOpts.AddGlobalCapability("os_version", "10");
                firefoxOpts.AddGlobalCapability("os", "Windows");
                firefoxOpts.AddGlobalCapability("os_version", "10");

                switch (browser)
                    {
                        case BrowserName.Chrome:
                            Driver = new RemoteWebDriver(Settings.BrowserStackUri, chromeOpts);
                        break;

                        case BrowserName.Firefox:
                            Driver = new RemoteWebDriver(Settings.BrowserStackUri, firefoxOpts);
                        break;
                    }
                }

            else
            {
                // Else prepare Uri base for new RemoteWebDriver, switch browser and return new RemoteWebDriver with new Uri of prepared base combined with selected browser port and capabilities
                switch (selectedEnvironment)
                {
                    case Environment.LocalGrid:
                        GridEnvironment = Settings.LocalGrid;
                        break;

                    case Environment.RemoteGrid:
                        GridEnvironment = Settings.RemoteGrid;
                        break;
                }

                switch (browser)
                {
                    case BrowserName.Chrome:
                        Driver = new RemoteWebDriver(new Uri($"{GridEnvironment}:{Settings.ChromePort}/wd/hub"), chromeOpts);
                        break;

                    case BrowserName.Firefox:
                        Driver = new RemoteWebDriver(new Uri($"{GridEnvironment}:{Settings.FirefoxPort}/wd/hub"), firefoxOpts);
                        break;
                }
            }
            return Driver;
        }
    }
}
