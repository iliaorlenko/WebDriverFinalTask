using System;
using System.Linq;

namespace WebDriverFinalTask.Base
{
    public static class StringGenerator
    {
        private readonly static Random _random = new Random();
        public static string GenerateString(int length)
        {
            const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
