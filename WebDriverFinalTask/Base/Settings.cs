using Newtonsoft.Json;
using System;
using System.IO;

namespace WebDriverFinalTask.Base
{
    public static class Settings
    {
        // Path to bin/Debug folder
        public static string baseDir = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppContext.BaseDirectory).ToString()).ToString()).ToString()).ToString();

        // Deserialized FrameworkConfig.json
        private static Configuration Config = JsonConvert.DeserializeObject<Configuration>(new StreamReader(baseDir + "/FrameworkConfig.json").ReadToEnd());
        public static string Grid => Config.Grid;
        public static string ChromePort => Config.ChromePort;
        public static string FirefoxPort => Config.FirefoxPort;
        public static string GmailLoginPageUrl => Config.GmailUrl;

        public static Environment? env = null;
        public static Uri HubUri
        {
            get
            {
                switch (env)
                {
                    case Environment.BrowserStack:
                        return new Uri(string.Format(Config.BrowserStack.Uri, Config.BrowserStack.User, Config.BrowserStack.Key));
                    case Environment.SauceLabs:
                        return new Uri(string.Format(Config.SauceLabs.Uri, Config.SauceLabs.User, Config.SauceLabs.Key));
                    case Environment.VM:
                        return new Uri(Config.VmNode);
                    default:
                        return null;
                }
            }
        }

        private class Configuration
        {
            [JsonProperty("BrowserStackSettings")]
            public BrowserStack BrowserStack { get; set; }

            [JsonProperty("SauceLabsSettings")]
            public SauceLabs SauceLabs { get; set; }

            [JsonProperty("Grid")]
            public string Grid { get; set; }

            [JsonProperty("VmNode")]
            public string VmNode { get; set; }

            [JsonProperty("ChromePort")]
            public string ChromePort { get; set; }

            [JsonProperty("FirefoxPort")]
            public string FirefoxPort { get; set; }

            [JsonProperty("GmailUrl")]
            public string GmailUrl { get; set; }
        }

        private class BrowserStack
        {
            [JsonProperty("User")]
            public string User { get; set; }

            [JsonProperty("Key")]
            public string Key { get; set; }

            [JsonProperty("Uri")]
            public string Uri { get; set; }
        }
        private class SauceLabs
        {
            [JsonProperty("User")]
            public string User { get; set; }

            [JsonProperty("Key")]
            public string Key { get; set; }

            [JsonProperty("Uri")]
            public string Uri { get; set; }
        }
    }
    public enum BrowserName
    {
        All,
        Chrome,
        Edge,
        Firefox,
        IE,
        Safari
    }

    public enum Environment
    {
        Local,
        VM,
        SauceLabs,
        BrowserStack
    }

    //public enum OS
    //{
    //    Android,
    //    Ios,
    //    Linux,
    //    Mac,
    //    Windows
    //}
}
