using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace WebDriverFinalTask.Base
{
    public static class Extensions
    {
        // Clicks by className of found IWebElement
        public static void JsClick(this IWebElement element, IWebDriver driver)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;

            executor.ExecuteScript($"document.getElementsByClassName('{element.GetAttribute("class")}')[0].click();");
        }


        // Adds capabilities for any kind of browser-specific options classes
        public static void AddGlobalCapability(this DriverOptions driverOptions, string capabilityName, string capabilityValue)
        {
            switch (driverOptions)
            {
                case ChromeOptions chromeOptions:
                    chromeOptions.AddAdditionalCapability(capabilityName, capabilityValue, true);
                    break;

                case FirefoxOptions firefoxOptions:
                    firefoxOptions.AddAdditionalCapability(capabilityName, capabilityValue, true);
                    break;

                case InternetExplorerOptions ieOptions:
                    ieOptions.AddAdditionalCapability(capabilityName, capabilityValue, true);
                    break;

                default:
                    driverOptions.AddAdditionalCapability(capabilityName, capabilityValue);
                    break;
            }
        }
    }
}
