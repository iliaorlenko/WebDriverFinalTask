﻿using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using WebDriverFinalTask.Base;

namespace WebDriverFinalTask.Pages
{
    public class MailPage : PageBase
    {
        public MailPage(IWebDriver Driver) : base(Driver) { }

        By NewMessageButtonLocator => By.XPath("//div[contains(text(), 'Написать')]");
        By LastMessageSubjectLocator => By.XPath("(//table[@role='grid'])[last()]//tr[1]/td[6]/div/div/div/span/span");
        By DeleteSelectedMessagesIconButtonLocator => By.XPath("(//div[@data-tooltip='Удалить'])[last()]");
        By DeleteSelectedMessagesTextButtonLocator => By.XPath("(//div[contains(text(), 'Удалить ')]/parent::div[@role='button'])[last()]");
        By SelectAllChainsButtonLocator => By.XPath("//span[contains (text(), 'Выбрать все цепочки')]");
        By RetryLoadingLinkLocator => By.XPath("//a[contains (text(), 'Повторить попытку')]");

        IWebElement EmailAddresseeTextbox => WaitFindElement(By.XPath("//textarea[contains (@aria-label, 'Кому')]"));
        IWebElement EmailSubjectTextbox => WaitFindElement(By.Name("subjectbox"));
        IWebElement EmailBodyTextbox => WaitFindElement(By.XPath("//div[contains (@aria-label, 'Тело письма')]"));
        IWebElement SendEmailButton => WaitFindElement(By.XPath("//div[contains(@data-tooltip, 'Отправить')]"));
        IWebElement ExpandCategoryPanelButton => WaitFindElement(By.XPath("//span[contains (text(), 'Ещё')]/parent::span[@role='button']"));
        IWebElement TrashBinButton => WaitFindElement(By.XPath("//div[@data-tooltip='Корзина']"));
        //public IWebElement DeleteLastReceivedEmailButton => WaitFindElement(By.XPath("(//table[@role='grid'])[2]//tr[1]//li[@data-tooltip='Удалить']//span"));
        IWebElement SelectAllMessagesCheckbox => WaitFindElement(By.XPath("(//div[@data-tooltip='Выбрать'])[last()]//span"));
        IWebElement NewMessageButton => WaitFindElement(NewMessageButtonLocator);
        IWebElement SelectAllChaisButton => WaitFindElement(SelectAllChainsButtonLocator);
        public IWebElement LastMessageBodyLabel => WaitFindElement(By.XPath("(//table[@role='grid'])[last()]//tr[1]/td[6]/div/div/span"));
        public IWebElement LastMessageSubjectLabel => WaitFindElement(LastMessageSubjectLocator);

        // Waits for sent email to appear in Inbox
        public MailPage WaitForSentEmail(string sentEmailSubject)
        {
            // Refresh page until sent email is displayed
            new WebDriverWait(Driver, TimeSpan.FromSeconds(60)).Until(cond =>
            {
                while (!ElementExists(LastMessageSubjectLocator) || LastMessageSubjectLabel.Text != sentEmailSubject)
                {
                    if (ElementExists(NewMessageButtonLocator))
                    {
                        Driver.Navigate().Refresh();
                    }
                    else
                        // Sometimes GMail redirecs to error page. In that case need to click the link from the condition below to try again
                        if (ElementExists(RetryLoadingLinkLocator))
                    {
                        Driver.FindElement(RetryLoadingLinkLocator).Click();
                    }
                    return false;
                }
                return true;
            });
            return this;
        }

        // Opens Trash Bin category
        public MailPage OpenTrashBin()
        {
            ExpandCategoryPanel();

            TrashBinButton.Click();

            new WebDriverWait(Driver, TimeSpan.FromSeconds(3)).Until(condition => Driver.Url.Contains("#trash"));

            return this;
        }

        // Deletes last received email and puts it in Trash Bin
        public MailPage DeleteLastReceivedEmail()
        {
            LastMessageSubjectLabel.Click();

            WaitFindElement(By.XPath("//div[@data-tooltip='Ещё']")).Click();

            WaitFindElement(By.XPath("//div[contains (text(), 'Удалить это письмо')]/parent::div/parent::div/parent::div")).Click();

            return this;
        }

        // Expands Categories panel on the left
        public MailPage ExpandCategoryPanel()
        {
            // Check if panel is already expanded and if not - expand it
            new WebDriverWait(Driver, TimeSpan.FromSeconds(3)).Until(condition =>
            {
                if (!ElementExists(By.XPath("//span[contains(text(), 'Свернуть')]")))
                {
                    ExpandCategoryPanelButton.Click();

                    Actions action = new Actions(Driver);

                    action
                        .ClickAndHold(WaitFindElement(By.XPath("//hr/parent::div")))
                        .MoveByOffset(1, 100)
                        .Release()
                        .Build()
                        .Perform();

                    return true;
                }
                return true;
            });

            return this;
        }

        // Clicks New Message button
        public MailPage StartNewEmail()
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(5)).Until(condition => ElementExists(NewMessageButtonLocator));

            NewMessageButton.Click();

            return this;
        }

        // Populates provided addressee value into 'To' field
        public MailPage PopulateEmailAddressee(string addressee)
        {
            EmailAddresseeTextbox.SendKeys(addressee);

            return this;
        }

        // Populates provided subject value into 'Email Subject' field
        public MailPage PopulateEmailSubject(string emailSubjectValue)
        {
            EmailSubjectTextbox.SendKeys(emailSubjectValue);

            return this;
        }

        // Populates provided body value into 'Email Body' field
        public MailPage PopulateEmailBody(string emailBodyValue)
        {
            EmailBodyTextbox.SendKeys(emailBodyValue);

            return this;
        }

        // Clicks 'Send Email' button and waits until confirmation appears
        public MailPage ClickSendEmailButton()
        {
            SendEmailButton.JsClick(Driver);

            new WebDriverWait(Driver, TimeSpan.FromSeconds(5)).Until(condition => WaitFindElement(By.XPath("//span[contains(text(), 'Письмо отправлено.')]")).Displayed);

            return this;
        }

        // Opens Sent category
        public MailPage OpenSentEmails()
        {
            WaitFindElement(By.XPath("//a[@title='Отправленные']")).Click();

            new WebDriverWait(Driver, TimeSpan.FromSeconds(3)).Until(condition => Driver.Url.Contains("#sent"));

            return this;
        }

        // Opens Draft category
        public MailPage OpenDrafts()
        {
            WaitFindElement(By.XPath("//a[@title='Черновики']")).Click();

            new WebDriverWait(Driver, TimeSpan.FromSeconds(3)).Until(condition => Driver.Url.Contains("#drafts"));

            return this;
        }

        // Removes all messages from current category. Can be used on TestBase's OneTimeTearDown to make clean-up
        public MailPage RemoveAllMessages()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));

            bool checkboxChecked;

            SelectAllMessagesCheckbox.Click();

            checkboxChecked = bool.Parse(SelectAllMessagesCheckbox.GetAttribute("aria-checked"));

            IWebElement removeButton = null;

            while (checkboxChecked)
                //Find appropriate Remove button
                if (ElementExists(DeleteSelectedMessagesTextButtonLocator))
                {
                    removeButton = Driver.FindElement(DeleteSelectedMessagesTextButtonLocator);
                }
                else
                    if (ElementExists(DeleteSelectedMessagesIconButtonLocator))
                {
                    removeButton = Driver.FindElement(DeleteSelectedMessagesIconButtonLocator);
                }
                else
                {
                    throw new Exception("Unknown remove button type.");
                }

            wait.Until(condition =>
            {
                if (ElementExists(SelectAllChainsButtonLocator))
                {
                    SelectAllChaisButton.Click();

                    removeButton.Click();

                    WaitFindElement(By.XPath("(//button[@name='ok'])[last()]")).Click();

                    return true;
                }
                else removeButton.Click();

                checkboxChecked = bool.Parse(SelectAllMessagesCheckbox.GetAttribute("aria-checked"));

                return true;
            });
            return this;
        }
    }
}
