using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Linq;
using WebDriverFinalTask.DataProvider;

namespace WebDriverFinalTask.Base
{
    public class TestBase : DriverContext
    {
        public BrowserName Browser { get; set; }
        public IWebDriver driver { get; set; }


        public TestBase SetupDriver(BrowserName browser)
        {
            Browser = browser;
            driver = GetDriver(browser);
            driver.Url = Settings.GmailLoginPageUrl;
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            return this;
        }

        public TestBase(BrowserName browser)
        {
            SetupDriver(browser);
        }

        [TearDown]
        public void GlobalTearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                driver.Quit();
                SetupDriver(Browser);
            }
        }

        [OneTimeTearDown]
        public void GlobalFixtureTearDown()
        {
            driver.Quit();
        }
    }
}
