using Allure.NUnit.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Html5;
using OpenQA.Selenium.Remote;
using WebDriverFinalTask.Base;
using WebDriverFinalTask.Pages;

namespace WebDriverFinalTask.Tests
{
    [TestFixture(BrowserName.Chrome)]
    [TestFixture(BrowserName.Firefox)]
    [Parallelizable(ParallelScope.Fixtures)]
    public class LoginTests : TestBase
    {
        public LoginTests(BrowserName browser) : base(browser) { }

        LoginPage loginPage;

        [SetUp]
        public void SetUpTest()
        {
            loginPage = new LoginPage(Driver);
        }

        [Test]
        [TestCase("jd5890662", @",=zso:a[u<,\=\;u")]
        [TestCase("jb3720380", @"Z;uNa>]}M6yZdMc+")]
        [TestCase("janesimmons981", "Yu3'nk^t@%d*U48\"")]
        [
            AllureSubSuite("Login functionality tests"),
            AllureSeverity(Allure.Commons.Model.SeverityLevel.Blocker),
            AllureLink("ID-1"),
            AllureTest("Verify login with valid credentials"),
            AllureOwner("Ilya Orlenko"),
        ]
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
        [
            AllureSubSuite("Login functionality tests"),
            AllureSeverity(Allure.Commons.Model.SeverityLevel.Blocker),
            AllureLink("ID-2"),
            AllureTest("Verify logout"),
            AllureOwner("Ilya Orlenko"),
        ]
        public void VerifyLogout(string username, string password)
        {
            loginPage.LoginToGmail(username, password)
                .OpenAccountPanel()
                .ClickLogoutButton();

            StringAssert.AreEqualIgnoringCase(loginPage.SelectAccountPanelHeading.Text, "Выберите аккаунт",
                $"data-email attribute of profileOptionButton does not contain expected username: {username}");
        }

        [TearDown]
        public void TestTearDown()
        {
            loginPage.ChangeAccount();
        }
    }
}
