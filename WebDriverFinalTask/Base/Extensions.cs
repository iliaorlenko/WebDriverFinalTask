using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using System;

namespace WebDriverFinalTask.Base
{
    public static class Extensions
    {
        public static void JsClick(this IWebElement element, IWebDriver driver)
        {
            //Actions actions = new Actions(driver);
            //actions.MoveToElement(element).Build().Perform();
            //IWebElement a = actions.MoveToElement(element);
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript($"document.getElementsByClassName('{element.GetAttribute("class")}')[0].click();");
        }

        // Extension method for adding capabilities for any kind of browser-specific options classes
        public static void AddGlobalCapability(this DriverOptions driverOptions, string capabilityName, string capabilityValue)
        {
            switch (driverOptions)
            {
                case ChromeOptions chromeOptions:
                    chromeOptions.AddAdditionalCapability(capabilityName, capabilityValue, true);
                    //chromeOptions.AddArgument("--headless");
                    //chromeOptions.AddArgument("--no-sandbox");
                    //chromeOptions.AddArgument("--disable-dev-shm-usage");
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
