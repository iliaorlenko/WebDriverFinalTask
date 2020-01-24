using NUnit.Framework;
using System.Collections;
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
        //IEnumerable a;

        [OneTimeSetUp]
        public void SetupFixture()
        {
            //a = DataProvider.DataProvider.GetTestCaseData();
            loginPage = new LoginPage(driver);
        }

        public LoginTests(BrowserName browser) : base(browser) { }

        [Test, TestCaseSource(typeof(DataProvider.DataProvider), nameof(DataProvider.DataProvider.LoginTestData))]
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
        //, TestCaseSource(typeof(DataProvider.DataProvider), nameof(DataProvider.DataProvider.GetTestCaseData))]
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
