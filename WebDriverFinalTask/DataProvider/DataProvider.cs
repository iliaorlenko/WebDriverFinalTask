using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using WebDriverFinalTask.Base;

namespace WebDriverFinalTask.DataProvider
{
    public static class DataProvider
    {
        //public static IEnumerable LoginTestData { get { return GetTestsData("LoginTests"); } }
        //public static IEnumerable LoginTestData { get { yield return GetTestCaseData(); } }
        //public static IEnumerable LoginTestData { get { return GetTestCaseData(); } }
        public static IEnumerable LoginTestData { get { return GetTestsData2(); } }

        //public static IEnumerable GetTestCaseData()
        //{
        //    object[] finalObject = null;

        //    //string testCaseName = testContext.Test.MethodName;
        //    string testCaseName = TestContext.CurrentContext.Test.ClassName;
        //    string shortClassName = testCaseName.Substring(testCaseName.LastIndexOf('.') + 1);

        //    using StreamReader reader = new StreamReader(Settings.baseDir + @$"\DataProvider\TestData.json");
        //    //using StreamReader reader = new StreamReader(Settings.baseDir + @$"\DataProvider\TestCaseData\{testCaseName}.json");

        //    JObject testData = JObject.Parse(reader.ReadToEnd());

        //    JArray testCaseDataObject = (JArray)testData["TestData"][shortClassName];

        //    List<KeyValuePair<string, string>> currentProps = new List<KeyValuePair<string, string>>();

        //    int counter = 0;

        //    finalObject = new object[testCaseDataObject.Children<JObject>().Count()];

        //    foreach (JObject testCaseObject in testCaseDataObject.Children<JObject>())
        //    {



        //        //foreach (JObject subObj in testCaseObject.Parent)
        //        //{

        //        JObject parsed = JObject.Parse(testCaseObject.ToString());
        //        var obj = new object[] { parsed.Properties().ToList() };
        //        finalObject[counter] = obj;

        //        counter++;
        //        //}

        //    }
        //    return finalObject;
        //}









        public static IEnumerable GetTestsData2()
        {
            string testName = TestContext.CurrentContext.Test.Name;

            using StreamReader reader = new StreamReader(Settings.baseDir + @"\DataProvider\TestData.json");

            var result = JObject.Parse(reader.ReadToEnd());

            var properties = result.Properties().First().Children()["EmailTests"].Children();

            foreach (var obj in properties)
            {
                if(obj.Children().Count() > 1)
                {
                    foreach(var prop in obj)
                    {

                    }
                }
                obj.Children().ToList();

            }

            JObject json = JObject.Parse(reader.ReadToEnd());

            JArray testCaseDataObject = (JArray)result["TestData"][testName];

            return
                from usertests in json["TestData"][testName]
                let username = usertests["Username"].ToString()
                let password = usertests["Password"].ToString()
                select new object[] { username, password };
        }












        public static IEnumerable GetTestsData(string TestType)
        {
            using StreamReader reader = new StreamReader(Settings.baseDir + @"\DataProvider\TestData.json");

            JObject json = JObject.Parse(reader.ReadToEnd());
            if (TestType == "LoginTests")
            {
                return
                    from usertests in json["TestData"]["LoginTests"]
                    let username = usertests["Username"].ToString()
                    let password = usertests["Password"].ToString()
                    select new object[] { username, password };
            }

            else
                if (TestType == "EmailTests")
            {
                return
                    from emailtests in json["TestData"]["EmailTests"]
                    let emailSubject = emailtests["emailSubject"].ToString()
                    let emailBody = emailtests["emailBody"].ToString()
                    let userEmail1 = emailtests["userEmail1"].ToString()
                    let password1 = emailtests["password1"].ToString()
                    let userEmail2 = emailtests["userEmail2"].ToString()
                    let password2 = emailtests["password2"].ToString()
                    select new object[] { userEmail1, password1, userEmail2, password2, emailSubject, emailBody };
                //select new object[] {userEmail1, password1};
            }

            else throw new Exception("Something wrong with requested test data. Check the TestType.");
        }
    }
}
