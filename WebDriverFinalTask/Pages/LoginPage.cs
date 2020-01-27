using OpenQA.Selenium;
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
            EmailTextbox.SendKeys(userEmail);
            return this;
        }

        public LoginPage SubmitUserEmail()
        {
            SubmitEmailButton.JsClick(driver);
            return this;
        }

        public LoginPage SetPassword(string password)
        {
            PasswordTextbox.SendKeys(password);
            return this;
        }

        public MailPage SubmitPassword()
        {
            SubmitPasswordButton.JsClick(driver);
            return new MailPage(driver);
        }

        public MailPage LoginToGmail(string userEmail, string password)
        {
            return
                SetUserEmail(userEmail)
                .SubmitUserEmail()
                .SetPassword(password)
                .SubmitPassword();
        }

        //public LoginPage Logout()
        //{
        //    OpenAccountPanel();
        //    LogoutButton.JsClick(driver);
        //    return this;
        //}

        //public LoginPage ChangeAccount()
        //{
        //    ChangeAccountButton.JsClick(driver);
        //    return this;
        //}
    }
}
