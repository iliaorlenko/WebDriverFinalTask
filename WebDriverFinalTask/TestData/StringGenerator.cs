using System;
using System.Linq;

namespace WebDriverFinalTask.TestData
{
    public static class StringGenerator
    {
        private static Random random = new Random();
        public static string GenerateString(int length)
        {
            const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
