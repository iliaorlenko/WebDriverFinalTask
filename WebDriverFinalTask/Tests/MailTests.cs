using Allure.NUnit.Attributes;
using NUnit.Framework;
using WebDriverFinalTask.Base;
using WebDriverFinalTask.Pages;
using WebDriverFinalTask.TestData;

namespace WebDriverFinalTask.Tests
{
    [TestFixture(BrowserName.Chrome)]
    [TestFixture(BrowserName.Firefox)]
    [Parallelizable(ParallelScope.Fixtures)]
    public class MailTests : TestBase
    {
        MailPage mailPage;

        public MailTests(BrowserName browser) : base(browser) { }

        [SetUp]
        public void TestSetUp()
        {
            mailPage = new LoginPage(Driver)
                .LoginToGmail("jd5890662", @",=zso:a[u<,\=\;u");
        }

        [TearDown]
        public void TestTearDown()
        {
            mailPage.Logout().ChangeAccount();
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
            mailPage
                .StartNewEmail()
                .PopulateEmailAddressee(addresseeEmail)
                .PopulateEmailSubject(StringGenerator.GenerateString(20))
                .PopulateEmailBody(StringGenerator.GenerateString(50))
                .ClickSendEmailButton()
                .OpenSentEmails();

            StringAssert.Contains(mailPage.AssertionValues.SentEmailSubject, mailPage.LastMessageSubjectLabel.Text);
            StringAssert.Contains(mailPage.AssertionValues.SentEmailBody, mailPage.LastMessageBodyLabel.Text);
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
            mailPage
                .StartNewEmail()
                .PopulateEmailAddressee(addresseeEmail)
                .PopulateEmailSubject(StringGenerator.GenerateString(20))
                .PopulateEmailBody(StringGenerator.GenerateString(50))
                .ClickSendEmailButton()
                .OpenSentEmails();
            
        }

        [Test]
        [TestCase("jb3720380@gmail.com")]
        [
            AllureSubSuite("Mail functionality tests"),
            AllureSeverity(Allure.Commons.Model.SeverityLevel.Blocker),
            AllureLink("ID-1"),
            AllureTest("Verify deleted email put to trash folder"),
            AllureOwner("Ilya Orlenko"),
        ]
        public void VerifyDeleteEmail(string addresseeEmail)
        {
            mailPage
                .SendEmail(addresseeEmail, StringGenerator.GenerateString(20), StringGenerator.GenerateString(50))
                .Logout()
                .ChangeAccount()
                .LoginToGmail(addresseeEmail, @"Z;uNa>]}M6yZdMc+")
                .StoreLastReceivedEmailData()
                .DeleteLastReceivedEmail()
                .OpenTrashBin();

            StringAssert.Contains(mailPage.AssertionValues.LastReceivedEmailSubject, mailPage.LastMessageSubjectLabel.Text);
            StringAssert.Contains(mailPage.AssertionValues.LastReceivedEmailBody, mailPage.LastMessageBodyLabel.Text);

        }
    }
}
