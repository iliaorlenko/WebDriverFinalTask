using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using WebDriverFinalTask.Base;

namespace WebDriverFinalTask.Pages
{
    public class LoginPage : PageBase
    {
        public LoginPage(IWebDriver driver) : base(driver) { }

        By NextButtonLocator => By.XPath("//span[contains (text(), 'Далее')]/ancestor::div[@role='button']");

        IWebElement EmailTextbox => WaitFindElement(By.XPath("//input[@type='email']"));
        IWebElement PasswordTextbox => WaitFindElement(By.XPath("//input[@type='password']"));
        IWebElement NextButton => WaitFindElement(NextButtonLocator);

        public LoginPage SetUserEmail(string userEmail)
        {
            EmailTextbox.SendKeys(userEmail);

            return this;
        }

        public LoginPage SubmitUserEmail()
        {
            NextButton.JsClick(Driver);

            return this;
        }

        public LoginPage SetPassword(string password)
        {
            PasswordTextbox.SendKeys(password);

            new WebDriverWait(Driver, TimeSpan.FromSeconds(2)).Until(condition => ElementExists(NextButtonLocator));

            return this;
        }

        public MailPage SubmitPassword()
        {
            NextButton.Click();

            return new MailPage(Driver);
        }

        public MailPage LoginToGmail(string userEmail, string password)

        {
            return
                SetUserEmail(userEmail)
                .SubmitUserEmail()
                .SetPassword(password)
                .SubmitPassword();
        }
    }
}
