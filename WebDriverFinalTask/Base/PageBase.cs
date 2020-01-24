using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using WebDriverFinalTask.Pages;

namespace WebDriverFinalTask.Base
{
    public class PageBase
    {
        protected IWebDriver driver;

        public PageBase(IWebDriver driver) { this.driver = driver; }

        public IWebElement LogoutButton => WaitFindElement(By.XPath("//a[@id='gb_71']"));
        public IWebElement SelectAccountPanelHeading => WaitFindElement(By.XPath("//span[contains(text(), 'Выберите аккаунт')]"));
        public IWebElement ChangeAccountButton => WaitFindElement(By.XPath("//div[contains(text(), 'Сменить аккаунт')]"));
        public IWebElement AccountPanelButton => WaitFindElement(By.XPath("//a[contains (@aria-label, 'Аккаунт')]"));

        public MailPage OpenAccountPanel()
        {
            AccountPanelButton.JsClick(driver);
            return new MailPage(driver);
        }

        public MailPage ClickLogoutButton()
        {
            LogoutButton.JsClick(driver);
            return new MailPage(driver);
        }

        public LoginPage Logout()
        {
            OpenAccountPanel();
            LogoutButton.JsClick(driver);
            return new LoginPage(driver);
        }

        public LoginPage ChangeAccount()
        {
            ChangeAccountButton.JsClick(driver);
            return new LoginPage(driver);
        }

        // Wait and get element
        public IWebElement WaitFindElement(By locator)
        {
            // Set null as a default value for element expected to return
            IWebElement ExpectedElement = null;

            // Initialize instance of explicit wait 
            WebDriverWait Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(11));

            // Set custom polling interval
            Wait.PollingInterval = TimeSpan.FromMilliseconds(200);

            // Set condition for explicit wait
            Wait.Until(condition =>
            {
                try
                {
                    ExpectedElement = driver.FindElement(locator);

                    if (ExpectedElement.Displayed && ExpectedElement.Enabled)
                    {
                        return ExpectedElement.Displayed;
                    }

                    else return false;
                }
                catch (StaleElementReferenceException ex)
                {
                    Debug.WriteLine(ex.Message);
                    return false;
                }
                catch (NoSuchElementException ex)
                {
                    Debug.WriteLine(ex.Message);
                    return false;
                }
            });
            // Return expected element
            return ExpectedElement;
        }

    }
}

