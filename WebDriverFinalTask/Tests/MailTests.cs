using Allure.NUnit.Attributes;
using NUnit.Framework;
using WebDriverFinalTask.Base;
using WebDriverFinalTask.Pages;

namespace WebDriverFinalTask.Tests
{
    [TestFixture(BrowserName.Chrome)]
    [TestFixture(BrowserName.Firefox)]
    [NonParallelizable]
    public class MailTests : TestBase
    {
        LoginPage _loginPage;
        MailPage _mailPage;

        public MailTests(BrowserName browser) : base(browser) { }

        [SetUp]
        public void TestSetUp()
        {
            _loginPage = new LoginPage(Driver);
            _mailPage = _loginPage.LoginToGmail("jd5890662", @",=zso:a[u<,\=\;u");
        }

        [TearDown]
        public void TestTearDown()
        {
            _mailPage.Logout().ChangeAccount();
        }

        [Test]
        [TestCase("jb3720380@gmail.com")]
        [
            AllureSubSuite("Mail functionality tests"),
            AllureSeverity(Allure.Commons.Model.SeverityLevel.Blocker),
            AllureLink("ID-1"),
            AllureTest("Verify email is sent"),
            AllureOwner("Ilya Orlenko"),
        ]
        public void VerifyEmailIsSent(string addresseeEmail)
        {
            string emailSubject = StringGenerator.GenerateString(20);
            string emailBody = StringGenerator.GenerateString(50);
            string assertMessage = "Sent email doesn't contains either expected subject or expected body";

            _mailPage
                .StartNewEmail()
                .PopulateEmailAddressee(addresseeEmail)
                .PopulateEmailSubject(emailSubject)
                .PopulateEmailBody(emailBody)
                .ClickSendEmailButton()
                .Logout()
                .ChangeAccount();

            _loginPage
                .LoginToGmail(addresseeEmail, @"Z;uNa>]}M6yZdMc+")
                .WaitForSentEmail(emailSubject);

            StringAssert.Contains(emailSubject, _mailPage.LastMessageSubjectLabel.Text, assertMessage);
            StringAssert.Contains(emailBody, _mailPage.LastMessageBodyLabel.Text, assertMessage);
        }

        [Test]
        [TestCase("jb3720380@gmail.com")]
        [
            AllureSubSuite("Mail functionality tests"),
            AllureSeverity(Allure.Commons.Model.SeverityLevel.Blocker),
            AllureLink("ID-1"),
            AllureTest("Verify sent email put to sent folder"),
            AllureOwner("Ilya Orlenko"),
        ]
        public void VerifySentEmailPutToSentFolder(string addresseeEmail)
        {
            string emailSubject = StringGenerator.GenerateString(20);
            string emailBody = StringGenerator.GenerateString(50);

            _mailPage
                .StartNewEmail()
                .PopulateEmailAddressee(addresseeEmail)
                .PopulateEmailSubject(emailSubject)
                .PopulateEmailBody(emailBody)
                .ClickSendEmailButton()
                .OpenSentEmails();

            StringAssert.Contains(emailSubject, _mailPage.LastMessageSubjectLabel.Text);
            StringAssert.Contains(emailBody, _mailPage.LastMessageBodyLabel.Text);
        }

        [Test]
        [TestCase("jb3720380@gmail.com"), NonParallelizable]
        [
            AllureSubSuite("Mail functionality tests"),
            AllureSeverity(Allure.Commons.Model.SeverityLevel.Blocker),
            AllureLink("ID-1"),
            AllureTest("Verify deleted email put to trash folder"),
            AllureOwner("Ilya Orlenko"),
        ]
        public void VerifyDeleteEmail(string addresseeEmail)
        {
            string emailSubject = StringGenerator.GenerateString(20);
            string emailBody = StringGenerator.GenerateString(50);

            _mailPage
                .StartNewEmail()
                .PopulateEmailAddressee(addresseeEmail)
                .PopulateEmailSubject(emailSubject)
                .PopulateEmailBody(emailBody)
                .ClickSendEmailButton()
                .Logout()
                .ChangeAccount();

            _loginPage
                .LoginToGmail(addresseeEmail, @"Z;uNa>]}M6yZdMc+");

            _mailPage.WaitForSentEmail(emailSubject)
                .DeleteLastReceivedEmail()
                .OpenTrashBin();

            StringAssert.Contains(emailSubject, _mailPage.LastMessageSubjectLabel.Text);
            StringAssert.Contains(emailBody, _mailPage.LastMessageBodyLabel.Text);
        }
    }
}
