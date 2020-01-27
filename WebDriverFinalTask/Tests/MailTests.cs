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
            mailPage = new LoginPage(Driver).LoginToGmail("jd5890662", @",=zso:a[u<,\=\;u");
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
                .PopulateEmailSubject()
                .PopulateEmailBody()
                .SendEmail()
                .OpenSentEmails();

            System.Threading.Thread.Sleep(3000);
            StringAssert.Contains(CurrentValues.EmailSubject, mailPage.SentEmailSubject.Text);
            StringAssert.Contains(CurrentValues.EmailBody, mailPage.SentEmailBody.Text);
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
                .PopulateEmailSubject()
                .PopulateEmailBody()
                .SendEmail()
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
                .StartNewEmail()
                .PopulateEmailAddressee(addresseeEmail)
                .PopulateEmailSubject()
                .PopulateEmailBody()
                .SendEmail();
        }
    }
}
