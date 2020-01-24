using NUnit.Framework;
using WebDriverFinalTask.Base;
using WebDriverFinalTask.Pages;

namespace WebDriverFinalTask.Tests
{
    [TestFixture(BrowserName.Chrome)]
    [TestFixture(BrowserName.Firefox)]
    [Parallelizable(ParallelScope.Fixtures)]
    public class LoginTests : TestBase
    {
        LoginPage loginPage;

        [OneTimeSetUp]
        public void SetupFixture()
        {
            loginPage = new LoginPage(driver);
        }

        public LoginTests(BrowserName browser) : base(browser) { }

        [Test]
        [TestCase("jd5890662", @",=zso:a[u<,\=\;u")]
        [TestCase("jb3720380", @"Z;uNa>]}M6yZdMc+")]
        [TestCase("janesimmons981", "Yu3'nk^t@%d*U48\"")]
        public void VerifyLoginWithValidCredentials(string username, string password)
        {

            loginPage.SetUserEmail(username)
                .SubmitUserEmail()
                .SetPassword(password)
                .SubmitPassword();

            Assert.True(loginPage.AccountPanelButton.Displayed, $"The page does not contain AccountPanelButton element.");

            loginPage.Logout();
        }

        [Test]
        [TestCase("jd5890662", @",=zso:a[u<,\=\;u")]
        [TestCase("jb3720380", @"Z;uNa>]}M6yZdMc+")]
        [TestCase("janesimmons981", "Yu3'nk^t@%d*U48\"")]
        public void VerifyLogout(string username, string password)
        {
            loginPage.LoginToGmail(username, password)
                .OpenAccountPanel()
                .ClickLogoutButton();

            StringAssert.AreEqualIgnoringCase(loginPage.SelectAccountPanelHeading.Text, "Выберите аккаунт",
                $"data-email attribute of profileOptionButton does not contain expected username: {username}");
        }

        [TearDown]
        public void TeardownTest()
        {
            loginPage.ChangeAccount();
        }
    }
}
