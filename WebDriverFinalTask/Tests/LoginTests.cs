using Allure.NUnit.Attributes;
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
        public LoginTests(BrowserName browser) : base(browser) { }

        LoginPage _loginPage;

        [SetUp]
        public void SetUpTest()
        {
            _loginPage = new LoginPage(Driver);
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
            string AssertMessage = "The page does not contain AccountPanelButton element.";

            _loginPage.SetUserEmail(username)
                .SubmitUserEmail()
                .SetPassword(password)
                .SubmitPassword();

            Assert.True(_loginPage.AccountPanelButton.Displayed, AssertMessage);

            _loginPage.Logout();
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
            string ExpectedResult = "Выберите аккаунт";
            string AssertMessage = $"data-email attribute of profileOptionButton does not contain expected username: {username}";

            _loginPage.LoginToGmail(username, password)
                .OpenAccountPanel()
                .ClickLogoutButton();

            StringAssert.AreEqualIgnoringCase(_loginPage.SelectAccountPanelHeading.Text, ExpectedResult, AssertMessage);
        }

        [TearDown]
        public void TestTearDown()
        {
            _loginPage.ChangeAccount();
        }
    }
}
