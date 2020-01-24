using NUnit.Framework;
using WebDriverFinalTask.Base;
using WebDriverFinalTask.Pages;

namespace WebDriverFinalTask.Tests
{
    [TestFixture(BrowserName.Chrome)]
    [TestFixture(BrowserName.Firefox)]
    //[TestFixtureSource(typeof(TestBase), nameof(EmailTestData))]
    [Parallelizable(ParallelScope.Fixtures)]
    public class MailTests : TestBase
    {
        MailPage mailPage;

        public MailTests(BrowserName Browser) : base(Browser) { }

        [OneTimeSetUp]
        public void SetupFixture()
        {
            
        }
        [SetUp]
        public void Setup()
        {
            //DataProvider.DataProvider.GetTestCaseData(TestContext.CurrentContext);
        }

        [Test]
        //, TestCaseSource(typeof(DataProvider.DataProvider), nameof(DataProvider.DataProvider.EmailTestData))]
        public void VerifyEmailIsSent(string userEmail1, string password1, string userEmail2, string password2, string emailBody, string emailSubject)
        {
            mailPage = new LoginPage(driver)
                .LoginToGmail(userEmail1, password1)
                .StartNewEmail()
                .PopulateAddressee(userEmail2)
                .PopulateEmailSubject()
                .PopulateEmailBody()
                .SendEmail()
                .OpenSentEmails();

            System.Threading.Thread.Sleep(10000);
            Assert.True(mailPage.SentEmailSubject.Text == DataProvider.CurrentValues.EmailSubject && mailPage.SentEmailBody.Text == DataProvider.CurrentValues.EmailBody);
        }

        [Test]
        //, TestCaseSource(typeof(DataProvider.DataProvider), nameof(DataProvider.DataProvider.EmailTestData))]
        public void VerifySentEmailPutToSentFolder(string userEmail1, string password1, string userEmail2, string password2, string emailBody, string emailSubject)
        {
            mailPage = new LoginPage(driver)
                   .LoginToGmail(userEmail1, password1);

        }

        [Test]
        //[TestCaseSource(typeof(TestBase), nameof(EmailTestData))]
        public void VerifyDeleteEmail(string userEmail1, string password1, string userEmail2, string password2, string emailBody, string emailSubject)
        {
            new LoginPage(driver)
                .SetUserEmail(userEmail1)
                .SubmitUserEmail()
                .SetPassword(password1)
                .SubmitPassword()
                .ExpandCategoryPanel();
        }
    }
}
