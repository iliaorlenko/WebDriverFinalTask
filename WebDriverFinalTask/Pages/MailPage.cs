using OpenQA.Selenium;
using WebDriverFinalTask.Base;
using WebDriverFinalTask.TestData;

namespace WebDriverFinalTask.Pages
{
    public class MailPage : PageBase
    {
        public MailPage(IWebDriver driver) : base(driver) { }

        By LastSentEmailRow = By.XPath("");

        private IWebElement NewMessageButton => WaitFindElement(By.XPath("//div[contains(text(), 'Написать')]"));
        private IWebElement EmailAddresseeTextbox => WaitFindElement(By.XPath("//textarea[contains (@aria-label, 'Кому')]"));
        private IWebElement SentEmailsCategory => WaitFindElement(By.XPath("//a[@title='Отправленные']"));
        private IWebElement EmailSubjectTextbox => WaitFindElement(By.Name("subjectbox"));
        private IWebElement EmailBodyTextbox => WaitFindElement(By.XPath("//div[contains (@aria-label, 'Тело письма')]"));
        private IWebElement SendEmailButton => WaitFindElement(By.XPath("//div[contains(@data-tooltip, 'Отправить')]"));
        private IWebElement LastMailboxItemRow => WaitFindElement(By.XPath("//div[@role='main']//tr[1]"));
        private IWebElement ExpandCategoryPanelButton => WaitFindElement(By.XPath("//span[contains (text(), 'Ещё')]/parent::span[@role='button']"));
        private IWebElement TrashBinButton => WaitFindElement(By.XPath("//div[@title='Корзина']"));
        //public IWebElement ProfileEmailLabel => WaitFindElement(By.XPath("//div[@title='Профиль']/parent::div/following-sibling::div/child::div[2]"));
        //private IWebElement LatestSentEmailRow => WaitFindElement(By.XPath("//table[@role='grid']/child::tbody/child::tr[1]"));
        //public IWebElement SentEmailSubject => LatestSentEmailRow.(By.XPath("/child::td[@role='gridcell'][2]//child::span[@data-thread-id]"));
        //public IWebElement SentEmailBody => LatestSentEmailRow.FindElement(By.XPath("/child::td[@role='gridcell'][2]/div/div/child::span"));

        public IWebElement SentEmailSubject => WaitFindElement(By.XPath("//table[@role='grid']/child::tbody/child::tr[1]/child::td[@role='gridcell'][2]//child::span[@data-thread-id]"));
        public IWebElement SentEmailBody => WaitFindElement(By.XPath("//table[@role='grid']/child::tbody/child::tr[1]/child::td[@role='gridcell'][2]/div/div/child::span"));


        public MailPage ExpandCategoryPanel()
        {
            //IWebElement Panel = WaitFindElement(By.XPath("//h2[contains(text(), 'Ярлыки')]/parent::div"));
            //IWebElement hr = WaitFindElement(By.XPath("//hr/following-sibling::div"));
            //IWebElement target = WaitFindElement(By.XPath("//div[text(), 'Использовано']"));

            //Actions actions = new Actions(driver);
            //actions.MoveToElement(Panel)
            //    .DragAndDrop(hr, target)
            //    .Release()
            //    .Build()
            //    .Perform();

            ExpandCategoryPanelButton.Click();

            return this;
        }

        public MailPage StartNewEmail()
        {
            //string a = NewMessageButton.GetAttribute("class");
            //Actions actions = new Actions(driver);
            //actions.MoveToElement(NewMessageButton).Build().Perform();
            //string b = NewMessageButton.GetAttribute("class");
            //NewMessageButton.JsClick(driver);
            NewMessageButton.Click();
            return this;
        }

        public MailPage PopulateEmailAddressee(string addressee)
        {
            EmailAddresseeTextbox.SendKeys(addressee);
            return this;
        }

        public MailPage PopulateEmailBody()
        {
            string body = StringGenerator.GenerateString(100);
            EmailBodyTextbox.SendKeys(body);
            CurrentValues.EmailBody = body;

            return this;
        }

        public MailPage PopulateEmailSubject()
        {
            string subject = StringGenerator.GenerateString(20);
            EmailSubjectTextbox.SendKeys(subject);
            CurrentValues.EmailSubject = subject;

            return this;
        }

        public MailPage SendEmail()
        {
            SendEmailButton.JsClick(driver);
            return this;
        }

        public MailPage OpenSentEmails()
        {
            ExpandCategoryPanel();
            SentEmailsCategory.Click();

            return this;
        }

        public MailPage GetLastSentEmailBody()
        {

            return this;
        }
    }
}
