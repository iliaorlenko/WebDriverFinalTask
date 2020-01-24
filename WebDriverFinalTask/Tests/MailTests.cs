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

        public MailTests(BrowserName Browser) : base(Browser) { }

        [OneTimeSetUp]
        public void SetupFixture()
        {
            
        }
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCase("jd5890662", @",=zso:a[u<,\=\;u", "jb3720380@gmail.com")]
        public void VerifyEmailIsSent(string username, string password, string addresseeEmail)
        {
            mailPage = new LoginPage(driver)
                .LoginToGmail(username, password)
                .StartNewEmail()
                .PopulateEmailAddressee(addresseeEmail)
                .PopulateEmailSubject()
                .PopulateEmailBody()
                .SendEmail()
                .OpenSentEmails();

            //System.Threading.Thread.Sleep(2000);
            StringAssert.Contains(CurrentValues.EmailSubject, mailPage.SentEmailSubject.Text);
            StringAssert.Contains(CurrentValues.EmailBody, mailPage.SentEmailBody.Text);
        }

        [Test]
        [TestCase("jd5890662", @",=zso:a[u<,\=\;u")]
        public void VerifySentEmailPutToSentFolder(string username, string password)
        {
            mailPage = new LoginPage(driver)
                   .LoginToGmail(username, password);

        }

        [Test]
        [TestCase("jd5890662", @",=zso:a[u<,\=\;u")]
        public void VerifyDeleteEmail(string username, string password)
        {
            new LoginPage(driver)
                .SetUserEmail(username)
                .SubmitUserEmail()
                .SetPassword(password)
                .SubmitPassword()
                .ExpandCategoryPanel();
        }
    }
}
