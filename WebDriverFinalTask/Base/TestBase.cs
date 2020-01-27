﻿using Allure.Commons;
using Allure.Commons.Model;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Html5;
using OpenQA.Selenium.Remote;
using System;
using System.IO;

namespace WebDriverFinalTask.Base
{
    public class TestBase : DriverContext
    {
        BrowserName currentBrowser;

        protected RemoteWebDriver Driver { get; set; }

        public TestBase(BrowserName browser)
        {
            currentBrowser = browser;
            SetupDriver(browser);
        }

        public void SetupDriver(BrowserName browser)
        {
            Driver = GetDriver(browser);
            Driver.Url = Settings.GmailLoginPageUrl;
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }

        [OneTimeSetUp]
        public void GlobalFixturesSetUp()
        {
            string env = TestContext.Parameters.Get("env");
            selectedEnvironment = (Environment)Enum.Parse(typeof(Environment), env, true);
        }

        [SetUp]
        public void GlobalTestsSetUp()
        {

        }

        [TearDown]
        public void GlobalTestsTearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                AllureLifecycle.Instance.UpdateTestCase(x => x.descriptionHtml += $"<p>Date/Time: {DateTime.Now}</p>");
                AllureLifecycle.Instance.UpdateTestCase(x => x.descriptionHtml += $"<p>Browser: {Driver.Capabilities["browserName"]}</p>");
                AllureLifecycle.Instance.UpdateTestCase(x => x.descriptionHtml += $"<p>Platform: {Driver.Capabilities["platformName"]}</p>");
                AllureLifecycle.Instance.UpdateTestCase(x => x.descriptionHtml += $"<p>Platform Version: {System.Environment.OSVersion}</p>");

                AllureLifecycle.Instance.AddAttachment(
                    $"{TestContext.CurrentContext.Test.Name} [{DateTime.Now:HH:mm:ss}]",
                    "image/png",
                    ((ITakesScreenshot)Driver).GetScreenshot().AsByteArray);

                Driver.Quit();
                SetupDriver(currentBrowser);
            }
        }

        [OneTimeTearDown]
        public void GlobalFixturesTearDown()
        {
            Driver.Quit();
        }

        public void TakeScreenshot()
        {
            string dirName = Settings.ScreenshotPath;
            string fileName = "Screenshot " + DateTime.Now.ToString("MM-dd-yyy hh_mm_ss tt") + ".png";

            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();

            screenshot.SaveAsFile(dirName + fileName);
        }
    }
}
