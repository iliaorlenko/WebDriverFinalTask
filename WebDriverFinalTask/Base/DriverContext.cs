using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;

namespace WebDriverFinalTask.Base
{
    public class DriverContext
    {
        public RemoteWebDriver GetDriver(BrowserName Browser)
        {
            //Uri firefoxNode = new Uri("http://localhost:5551/wd/hub");
            //Uri chromeNode = new Uri("http://localhost:5552/wd/hub");

            Uri firefoxNode = new Uri($"{Settings.Grid}:{Settings.ChromePort}/wd/hub");
            Uri chromeNode = new Uri($"{Settings.Grid}:{Settings.ChromePort}/wd/hub");

            FirefoxOptions firefoxOpts = new FirefoxOptions();
            ChromeOptions chromeOpts = new ChromeOptions();
            chromeOpts.AddArgument("--start-maximized");
            //chromeOpts.AddArguments("--disable-dev-shm-usage");

            switch (Browser)
            {
                case BrowserName.Firefox:
                    return new RemoteWebDriver(firefoxNode, firefoxOpts);
                default:
                    return new RemoteWebDriver(chromeNode, chromeOpts);
            }
        }
    }
}
