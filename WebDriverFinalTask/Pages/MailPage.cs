using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using WebDriverFinalTask.Base;
using WebDriverFinalTask.TestData;

namespace WebDriverFinalTask.Pages
{
    public class MailPage : PageBase
    {
        public MailPage(IWebDriver driver) : base(driver) { }

        private IWebElement NewMessageButton => WaitFindElement(By.XPath("//div[contains(text(), 'Написать')]"));
        private IWebElement EmailAddresseeTextbox => WaitFindElement(By.XPath("//textarea[contains (@aria-label, 'Кому')]"));
        private IWebElement EmailSubjectTextbox => WaitFindElement(By.Name("subjectbox"));
        private IWebElement EmailBodyTextbox => WaitFindElement(By.XPath("//div[contains (@aria-label, 'Тело письма')]"));
        private IWebElement SendEmailButton => WaitFindElement(By.XPath("//div[contains(@data-tooltip, 'Отправить')]"));
        private IWebElement ExpandCategoryPanelButton => WaitFindElement(By.XPath("//span[contains (text(), 'Ещё')]/parent::span[@role='button']"));
        private IWebElement TrashBinButton => WaitFindElement(By.XPath("//div[@data-tooltip='Корзина']"));
        public IWebElement LastMessageSubjectLabel => WaitFindElement(By.XPath("(//table[@role='grid'])[4]//td[6]/div/div/div/span/span"));
        public IWebElement LastMessageBodyLabel => WaitFindElement(By.XPath("(//table[@role='grid'])[4]//td[6]/div/div/span"));
        public IWebElement LastReceivedMessageSubjectLabel => WaitFindElement(By.XPath("(//table[@role='grid'])[2]//td[6]/div/div/div/span/span"));
        public IWebElement LastReceivedMessageBodyLabel => WaitFindElement(By.XPath("(//table[@role='grid'])[2]//td[6]/div/div/span"));
        public IWebElement DeleteLastReceivedEmailButton => WaitFindElement(By.XPath("(//table[@role='grid'])[2]//tr[1]//li[@data-tooltip='Удалить']//span"));
        public IWebElement SelectAllMessagesCheckbox => WaitFindElement(By.XPath("(//div[@data-tooltip='Выбрать'])[last()]//span"));
        public IWebElement DeleteSelectedMessagesButton => WaitFindElement(By.XPath("(//div[@data-tooltip='Удалить'])"));
        public IWebElement DeleteSelectedMessagesFromTrashBinButton => WaitFindElement(By.XPath("//div[contains(text(), 'Удалить навсегда')]/parent::div"));
        public IWebElement LastMessageSubjectInTrashBin => WaitFindElement(By.XPath("(//table[@role='grid'])[4]//tr[last()]/td[6]/div/div/div/span/span"));
        public IWebElement LastMessageBodyInTrashBin => WaitFindElement(By.XPath("(//table[@role='grid'])[4]//tr[last()]/td[6]/div/div/span"));

        public MailPage StoreLastReceivedEmailData()
        {
            driver.Navigate().Refresh();
            AssertionValues.LastReceivedEmailSubject = LastReceivedMessageSubjectLabel.Text;
            AssertionValues.LastReceivedEmailBody = LastReceivedMessageBodyLabel.Text;
            return this;
        }

        public MailPage OpenTrashBin()
        {
            TrashBinButton.Click();
            return this;
        }

        public MailPage DeleteLastReceivedEmail()
        {
            ExpandCategoryPanel();

            LastReceivedMessageSubjectLabel.Click();

            WaitFindElement(By.XPath("//div[@data-tooltip='Ещё']")).Click();

            WaitFindElement(By.XPath("//div[contains (text(), 'Удалить это письмо')]/parent::div/parent::div/parent::div")).Click();

            return this;
        }

        public MailPage ExpandCategoryPanel()
        {
            ExpandCategoryPanelButton.Click();

            Actions action = new Actions(driver);

            action
                .ClickAndHold(WaitFindElement(By.XPath("//hr/parent::div")))
                .MoveByOffset(1, 100)
                .Release()
                .Build()
                .Perform();

            return this;
        }

        public MailPage StartNewEmail()
        {
            NewMessageButton.Click();

            return this;
        }

        public MailPage PopulateEmailAddressee(string addressee)
        {
            EmailAddresseeTextbox.SendKeys(addressee);

            return this;
        }

        public MailPage PopulateEmailBody(string emailBodyValue)
        {
            EmailBodyTextbox.SendKeys(emailBodyValue);

            AssertionValues.SentEmailBody = emailBodyValue;

            return this;
        }

        public MailPage PopulateEmailSubject(string emailSubjectValue)
        {
            EmailSubjectTextbox.SendKeys(emailSubjectValue);

            AssertionValues.SentEmailSubject = emailSubjectValue;

            return this;
        }

        public MailPage ClickSendEmailButton()
        {
            SendEmailButton.JsClick(driver);

            return this;
        }

        public MailPage OpenSentEmails()
        {
            WaitFindElement(By.XPath("//a[@title='Отправленные']")).Click();

            return this;
        }

        public MailPage SendEmail(string addressee, string emailSubject, string emailBody)
        {
            StartNewEmail();
            PopulateEmailAddressee(addressee);
            PopulateEmailSubject(emailSubject);
            PopulateEmailBody(emailBody);
            ClickSendEmailButton();

            return this;
        }

        public MailPage RemoveAllMessages()
        {
            bool checkboxStatus;

            do
            {
                SelectAllMessagesCheckbox.Click();

                checkboxStatus = bool.Parse(SelectAllMessagesCheckbox.GetAttribute("aria-checked"));

                if (checkboxStatus)
                {
                    DeleteSelectedMessagesButton.Click();
                }
            }
            while (checkboxStatus);

            return this;
        }

        public MailPage RemoveAllMessagesFromTrashBin()
        {
            bool checkboxStatus = true;

            ExpandCategoryPanel();

            OpenTrashBin();

            do
            {
                SelectAllMessagesCheckbox.Click();

                checkboxStatus = bool.Parse(SelectAllMessagesCheckbox.GetAttribute("aria-checked"));

                if (checkboxStatus)
                {
                    DeleteSelectedMessagesFromTrashBinButton.FindElement(By.XPath("./parent::div/parent::div")).Click();
                }
            }
            while (checkboxStatus);

            return this;
        }
    }
}
