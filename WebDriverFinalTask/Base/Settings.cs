using Newtonsoft.Json;
using System;
using System.IO;

namespace WebDriverFinalTask.Base
{
    public static class Settings
    {
        // Path to project folder
        public static string baseDir = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppContext.BaseDirectory).ToString()).ToString()).ToString()).ToString();

        // Deserialized FrameworkConfig.json
        private static Configuration Config = JsonConvert.DeserializeObject<Configuration>(new StreamReader(baseDir + "/FrameworkConfig.json").ReadToEnd());

        public static Uri BrowserStackUri => new Uri($"http://{Config.BrowserStack.User}:{Config.BrowserStack.Key}@hub-cloud.browserstack.com/wd/hub/");
        public static string LocalGrid => Config.LocalGrid;
        public static string RemoteGrid => Config.RemoteGrid;
        public static string ChromePort => Config.ChromePort;
        public static string FirefoxPort => Config.FirefoxPort;
        public static string GmailLoginPageUrl => Config.GmailUrl;
        public static string ScreenshotPath => Config.ScreenshotFolder;

        //public static Environment? env = null;
        //public static Uri HubUri
        //{
        //    get
        //    {
        //        switch (env)
        //        {
        //            case Environment.LocalGrid:
        //                return new Uri(Config.LocalGrid);
        //            case Environment.RemoteGrid:
        //                return new Uri(Config.RemoteGrid);
        //            case Environment.BrowserStack:
        //                return new Uri(string.Format(Config.BrowserStack.Uri, Config.BrowserStack.User, Config.BrowserStack.Key));
        //            default:
        //                return null;
        //        }
        //    }
        //}

        private class Configuration
        {
            [JsonProperty("BrowserStackSettings")]
            public BrowserStack BrowserStack { get; set; }

            [JsonProperty("SauceLabsSettings")]
            public SauceLabs SauceLabs { get; set; }

            [JsonProperty("LocalGrid")]
            public string LocalGrid { get; set; }

            [JsonProperty("RemoteGrid")]
            public string RemoteGrid { get; set; }

            [JsonProperty("ChromePort")]
            public string ChromePort { get; set; }

            [JsonProperty("FirefoxPort")]
            public string FirefoxPort { get; set; }

            [JsonProperty("GmailUrl")]
            public string GmailUrl { get; set; }

            [JsonProperty("ScreenshotFolder")]
            public string ScreenshotFolder { get; set; }
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
        LocalGrid,
        RemoteGrid,
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
