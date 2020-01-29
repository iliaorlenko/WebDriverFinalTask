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

        public IWebElement EmailTextbox => WaitFindElement(By.Id("identifierId"));
        public IWebElement SubmitEmailButton => WaitFindElement(By.Id("identifierNext"));
        public IWebElement PasswordTextbox => WaitFindElement(By.Name("password"));
        public IWebElement SubmitPasswordButton => WaitFindElement(By.Id("passwordNext"));

        public LoginPage SetUserEmail(string userEmail)
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(5)).Until(condition => Driver.FindElement(By.Id("identifierId")).Displayed);

            EmailTextbox.SendKeys(userEmail);

            return this;
        }

        public LoginPage SubmitUserEmail()
        {
            SubmitEmailButton.JsClick(Driver);

            return this;
        }

        public LoginPage SetPassword(string password)
        {
            PasswordTextbox.SendKeys(password);
            System.Threading.Thread.Sleep(2000);
            return this;
        }

        public MailPage SubmitPassword()
        {
            SubmitPasswordButton.JsClick(Driver);

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
