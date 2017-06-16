using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using System;
using System.Threading;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using System.IO;
using System.Configuration;

namespace AddTestDesc
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read App Setting
            string testPlanId = ConfigurationSettings.AppSettings["MTM_TestPlanId"];
            string testSuiteId = ConfigurationSettings.AppSettings["MTM_TestSuiteId"];
            string project = ConfigurationSettings.AppSettings["Project"];
            //Description.txt
            List<string> titleList = new List<string>();
            List<string> descList = new List<string>();
            string descriptionFile = Directory.GetCurrentDirectory() + "\\..\\..\\..\\MigrateTestCasesFromRailToManager\\" + "description.txt";
            IEnumerable<string> titleDesc = File.ReadLines(descriptionFile);
            foreach (string line in titleDesc)
            {

                string[] sep = new string[] { "\t" };
                string[] xxx = line.Split(sep, StringSplitOptions.None);

                if (xxx.Length == 2)
                {
                    titleList.Add(xxx[0]);
                    descList.Add(xxx[1]);
                }
            }

            string chromeDriverDir = Directory.GetCurrentDirectory() + "\\..\\..\\..\\MigrateTestCasesFromRailToManager\\";
            IWebDriver driver = new ChromeDriver(chromeDriverDir);
            //testManager login
            driver.Url = "https://vmob.visualstudio.com/"+project+"/_testManagement?planId=" + testPlanId + "&suiteId=" + testSuiteId + "&_a=tests";
            //driver.Url = "https://vmob.testrail.net/index.php?/suites/view/44&group_by=cases:section_id&group_order=asc&group_id=1133";
            driver.Navigate();
            driver.FindElement(By.Id("cred_userid_inputtext")).SendKeys("ella.sun@plexure.com");
            driver.FindElement(By.Id("cred_password_inputtext")).SendKeys("pa10086&11");
            driver.FindElement(By.Id("cred_sign_in_button")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.ClassName("aad_account_tile")).Click();
            driver.FindElement(By.Id("cred_sign_in_button")).Click();
            Thread.Sleep(3000);

            List<string> IdList = new List<string>();
            IWebElement firstCell = driver.FindElement(By.ClassName("test-view-grid-area")).FindElement(By.XPath("./div"));
            string idCol = firstCell.GetAttribute("id");
            Actions action = new Actions(driver);
            for (int index = 0; index < titleList.Count; index++)
            {
               
                IWebElement row = driver.FindElement(By.Id("row_" + idCol + "_" + index));
                string rowText = row.Text;
               
                char[] splitor = { '\r', '\n' };
                string[] cols = rowText.Split(splitor);
                //for (int i = 0; i < cols.Length; i++) Console.WriteLine("{0}:{1}", i, cols[i]);
                IdList.Add(cols[4]);
                row.Click();
                action.SendKeys(Keys.Down).Perform();
            }
            
           
           


           

            //need modify everytime
            //int intialId = Convert.ToInt32(ConfigurationSettings.AppSettings["intialId"]);
            //Actions action = new Actions(driver);
            for(int index=0; index<titleList.Count; index++)
            {
                int id = Convert.ToInt32(IdList[index]);
                string desc = descList[index];
                string title1 = titleList[index];
                driver.Url = "https://vmob.visualstudio.com/" + project + "/_workitems?id=" + id + "&_a=edit";
                driver.Navigate();
                Thread.Sleep(2000);
                ReadOnlyCollection<IWebElement> buttons = driver.FindElements(By.ClassName("page-button"));
                buttons[1].Click();
                Thread.Sleep(1000);
                action.SendKeys(Keys.Tab).SendKeys(Keys.Tab).SendKeys(Keys.Tab).Perform();
                //action.KeyDown(Keys.Control).SendKeys("A").KeyUp(Keys.Control).Perform();
                //Thread.Sleep(100);
                //action.SendKeys(Keys.Delete).Perform();
                action.SendKeys(desc).Perform();
                Thread.Sleep(100);
                action.KeyDown(Keys.Control).SendKeys("s").KeyUp(Keys.Control);
                action.Perform();
                Thread.Sleep(3000);
            
            }

            driver.Quit();
            

        }
    }
}
