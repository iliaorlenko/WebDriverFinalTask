using System.Collections.Generic;

namespace WebDriverFinalTask.DataProvider
{
    public class TestData
    {
        public IEnumerable<LoginTest> LoginTests { get; set; }
        public IEnumerable<LoginTest> LoginTests2 { get; set; }
        public IEnumerable<LoginTest> LoginTests3 { get; set; }
    }

    public class LoginTest
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}