using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using WebDriverFinalTask.Pages;

namespace WebDriverFinalTask.Base
{
    public class PageBase
    {
        protected IWebDriver Driver;
        protected PageBase(IWebDriver Driver) { this.Driver = Driver; }

        public IWebElement LogoutButton => WaitFindElement(By.XPath("//a[@id='gb_71']"));
        public IWebElement SelectAccountPanelHeading => WaitFindElement(By.XPath("//span[contains(text(), 'Выберите аккаунт')]"));
        public IWebElement ChangeAccountButton => WaitFindElement(By.XPath("//div[contains(text(), 'Сменить аккаунт')]/ancestor::div[@role='link']"));
        public IWebElement AccountPanelButton => WaitFindElement(By.XPath("//a[contains (@aria-label, 'Аккаунт')]"));

        // Opens Account panel and returns new MailPage
        public MailPage OpenAccountPanel()
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(10)).Until(condition => ElementExists(By.XPath("//div[@aria-label='Задачи']")));

            AccountPanelButton.JsClick(Driver);

            return new MailPage(Driver);
        }

        // Clicks Logout button and returns new LoginPage
        public LoginPage ClickLogoutButton()
        {
            LogoutButton.Click();

            new WebDriverWait(Driver, TimeSpan.FromSeconds(5)).Until(condition => ElementExists(By.XPath("//div[contains(text(), 'Сменить аккаунт')]")));

            return new LoginPage(Driver);
        }

        // Logs out and returns new LoginPage
        public LoginPage Logout()
        {
            OpenAccountPanel();

            LogoutButton.Click();

            return new LoginPage(Driver);
        }

        // Clicks Change Account button and returns new LoginPage
        public LoginPage ChangeAccount()
        {
            Actions actions = new Actions(Driver);

            actions.MoveToElement(ChangeAccountButton).Click().Build().Perform();

            return new LoginPage(Driver);
        }


        // Wait and get element
        public IWebElement WaitFindElement(By locator)
        {
            // Set null as a default value for element expected to return
            IWebElement ExpectedElement = null;

            // Initialize instance of explicit wait 
            WebDriverWait Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));

            // Set custom polling interval
            Wait.PollingInterval = TimeSpan.FromMilliseconds(200);

            // Set condition for explicit wait
            Wait.Until(condition =>
            {
                try
                {
                    ExpectedElement = Driver.FindElement(locator);

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

        // Checks if element with provided locator exists on a page
        public bool ElementExists(By locator)
        {
            try
            {
                Driver.FindElement(locator);
            }
            catch (NoSuchElementException ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}

