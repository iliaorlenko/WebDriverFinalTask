using OpenQA.Selenium;
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

        //public static IWebElement FindInElement(this IWebElement element)
        //{


        //}
    }
}
