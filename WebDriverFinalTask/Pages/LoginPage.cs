using NUnit.Framework;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Commands;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using WebDriverFinalTask.Base;

namespace WebDriverFinalTask.Pages
{
    public class LoginPage : PageBase
    {
        public LoginPage(IWebDriver driver) : base(driver) { }

        public IWebElement EmailTextbox => WaitFindElement(By.XPath("//input[@type='email']"));
        //public IWebElement SubmitEmailButton => WaitFindElement(By.Id("identifierNext"));
        public IWebElement PasswordTextbox => WaitFindElement(By.XPath("//input[@type='password']"));
        public IWebElement NextButton => WaitFindElement(By.XPath("//span[contains (text(), 'Далее')]/ancestor::div[@role='button']"));

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

            new WebDriverWait(Driver, TimeSpan.FromSeconds(2)).Until(condition => ElementExists(By.XPath("//span[contains (text(), 'Далее')]/ancestor::div[@role='button']")));

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
