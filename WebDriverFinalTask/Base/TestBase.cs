using Allure.Commons;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.IO;

namespace WebDriverFinalTask.Base
{
    public class TestBase : DriverContext
    {
        readonly BrowserName _currentBrowser;

        protected IWebDriver Driver { get; set; }

        // Constructor that takes console parameters and prepares appropriate driver
        public TestBase(BrowserName browser)
        {
            string env = TestContext.Parameters.Get("env", "Local");
            SelectedEnvironment = (Environment)Enum.Parse(typeof(Environment), env, true);
            _currentBrowser = browser;
            SetupDriver(browser);
        }

        // Method to setup appropriate driver
        public void SetupDriver(BrowserName browser)
        {
            Driver = GetDriver(browser);
            Driver.Url = Settings.GmailLoginPageUrl;
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
        }


        [TearDown]
        public void GlobalTestsTearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                ICapabilities capabilities = ((RemoteWebDriver)Driver).Capabilities;

                // Additional information for Allure report on failed tests
                AllureLifecycle.Instance.UpdateTestCase(x => x.descriptionHtml += $"<p>Date/Time: {DateTime.Now}</p>");
                AllureLifecycle.Instance.UpdateTestCase(x => x.descriptionHtml += $"<p>Browser: {capabilities.GetCapability("browserName")}</p>");
                AllureLifecycle.Instance.UpdateTestCase(x => x.descriptionHtml += $"<p>Platform: {capabilities.GetCapability("platformName")}</p>");
                AllureLifecycle.Instance.UpdateTestCase(x => x.descriptionHtml += $"<p>Platform Version: {System.Environment.OSVersion}</p>");

                // Add Allure screenshot to failed test items
                AllureLifecycle.Instance.AddAttachment( $"{TestContext.CurrentContext.Test.Name} [{DateTime.Now:HH:mm:ss}]", "image/png", ((ITakesScreenshot)Driver).GetScreenshot().AsByteArray);

                // If test failed close browser...
                Driver.Quit();

                // ... and start new test with new browser instance
                SetupDriver(_currentBrowser);
            }
        }


        [OneTimeTearDown]
        public void GlobalFixturesTearDown()
        {
            // Commented is global messages cleanup for all three accounts are used in tests
            if(_currentBrowser == BrowserName.Chrome)
            {
                //List<KeyValuePair<string, string>> allTestAccounts = new List<KeyValuePair<string, string>>()
                //{
                //    new KeyValuePair<string, string>("jd5890662", @",=zso:a[u<,\=\;u"),
                //    new KeyValuePair<string, string>("jb3720380", "Z;uNa>]}M6yZdMc+"),
                //    new KeyValuePair<string, string>("janesimmons981", "Yu3'nk^t@%d*U48\"")
                //};

                //LoginPage loginPage = new LoginPage(Driver);

                //foreach (KeyValuePair<string, string> account in allTestAccounts)
                //{

                //    loginPage
                //        .LoginToGmail(account.Key, account.Value)
                //        .RemoveAllMessages()
                //        .OpenSentEmails()
                //        .RemoveAllMessages()
                //        .OpenDrafts()
                //        .RemoveAllMessages()
                //        .OpenTrashBin()
                //        .RemoveAllMessages()
                //        .Logout()
                //        .ChangeAccount();
                //}
            }

            Driver.Quit();
        }

        // Method for taking local screenshots
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
