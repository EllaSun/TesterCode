using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using System;
using System.Threading;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using System.IO;
using System.Configuration;
namespace Move
{
    class Program
    {
        static void Main(string[] args)
        {
            //testRail login
            // Read App Setting
            string testRailSuiteId = ConfigurationSettings.AppSettings["TestRailSuiteId"];
            string testPlanId = ConfigurationSettings.AppSettings["MTM_TestPlanId"];
            string testSuiteId = ConfigurationSettings.AppSettings["MTM_TestSuiteId"];
            string sectionId = ConfigurationSettings.AppSettings["TestRailSectionId"];
            string project = ConfigurationSettings.AppSettings["Project"];

            // TestRail login
            string chromeDriverDir = Directory.GetCurrentDirectory() + "\\..\\..\\";


            //Description.txt
            string descriptionFile = Directory.GetCurrentDirectory() + "\\..\\..\\" + "description.txt";

            IWebDriver driver = new ChromeDriver(chromeDriverDir);
            driver.Url = "https://vmob.testrail.net/";
            driver.Navigate();
            string addressXpath = "//*[@id=\"name\"]";
            string pwdXpath = "//*[@id=\"password\"]";
            driver.FindElement(By.XPath(addressXpath)).SendKeys("hosting@vmob.com");
            driver.FindElement(By.XPath(pwdXpath)).SendKeys("cram372&REMs");
            string loginXpath = "//*[@id=\"content\"]/form/div[4]/button";
            driver.FindElement(By.XPath(loginXpath)).Click();

            driver.Url = "https://vmob.testrail.net/index.php?/suites/view/" + testRailSuiteId;
            driver.Navigate();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("filterByEmpty")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id=\"filter-cases:section_id\"]/div[1]/a[1]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id=\"filter-cases:section_id\"]/div[2]/select/option["+sectionId+"]")).Click();
            driver.FindElement(By.Id("filterCasesApply")).Click();
            //Get all url
            Thread.Sleep(2000);
            ReadOnlyCollection<IWebElement> urls = driver.FindElements(By.XPath("//tr/td[4]/a"));
            List<string> nameList = new List<string>();
            List<string> idList = new List<string>();
            List<string> preconditionList = new List<string>();
            List<List<string>> stepActionList = new List<List<string>>();
            List<List<string>> stepResultList = new List<List<string>>();
            List<string> urlxx = new List<string>();
            List<string> writeContent = new List<string>();

            foreach (IWebElement ele in urls)
            {
                string url = ele.GetAttribute("href");
                if (url.Contains("void")) continue;
                urlxx.Add(url);
            }
            foreach(string url in urlxx)
            { 
                Console.WriteLine("{0}", url);
                driver.Url = url;

                driver.Navigate();
                Thread.Sleep(1000);
                string name = driver.FindElement(By.XPath("//*[@id=\"content-header\"]/div/div[3]")).Text.Replace("\r\n"," ");
                string caseId = driver.FindElement(By.ClassName("content-header-id")).Text;
                string prediction = "Precondition:";
                string description = "";
                IWebElement body = driver.FindElement(By.XPath("//*[@id=\"content-inner\"]"));
                IWebElement stepSection;
                int stepLoc = 8;

                ReadOnlyCollection<IWebElement> descs = body.FindElements(By.ClassName("field-content"));
                ReadOnlyCollection<IWebElement> titles = body.FindElements(By.ClassName("field-title"));
                if (descs.Count == 3)
                {
                    description = description + descs[0].Text;
                    prediction = prediction + descs[1].Text;
                    stepLoc = 8;
                    stepSection = descs[2];
                }
                else if (descs.Count == 2)
                {
                    //check it is desc or precondition
                    if (titles[0].Text == "Description")
                    {
                        description = description + descs[0].Text;
                        char[] skip = { '\r', '\n'};
                        description.Trim(skip);
                    }
                    else
                    {
                        prediction = prediction + descs[0].Text;
                    }
                    stepLoc = 6;
                    stepSection = descs[1];
                }
                else if (descs.Count == 1)
                {
                    stepLoc = 4;
                    stepSection = descs[0];
                }
                else
                {
                    stepSection = null;
                    stepActionList.Add(new List<string> ());
                    stepResultList.Add(new List<string>());
                }
                if (stepSection != null)
                {
                    string xpath = "//*[@id=\"content-inner\"]/div["+stepLoc+"]/table/tbody/tr";
                    ReadOnlyCollection<IWebElement> stepConut = stepSection.FindElements(By.XPath(xpath));
                    prediction = prediction.Replace("\r\n", " ");
                    List<string> stepActions = new List<string>();
                    List<string> stepResults = new List<string>();
                    for (int i = 1; i < stepConut.Count; i++)
                    {
                        int temp = 1 + i;
                        stepActions.Add(stepSection.FindElement(By.XPath(xpath+"["+temp+"]/td[2]")).Text.Replace("\r\n", " "));
                        stepResults.Add(stepSection.FindElement(By.XPath(xpath + "[" + temp + "]/td[3]")).Text.Replace("\r\n", " ")); 
                    }
                    stepActionList.Add(stepActions);
                    stepResultList.Add(stepResults);
                }

                nameList.Add(name);
                idList.Add(caseId);
                preconditionList.Add(prediction);
                writeContent.Add(name + "\t" + description);
            }
            File.WriteAllLines(descriptionFile, writeContent);

            //testManager login
             driver.Url = "https://vmob.visualstudio.com/"+project+"/_testManagement?planId=" + testPlanId + "&suiteId=" + testSuiteId + "&_a=tests";
            driver.Navigate();
            driver.FindElement(By.Id("cred_userid_inputtext")).SendKeys("ella.sun@plexure.com");
            driver.FindElement(By.Id("cred_password_inputtext")).SendKeys("pa10086&11");
            driver.FindElement(By.Id("cred_sign_in_button")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.ClassName("aad_account_tile")).Click();
            driver.FindElement(By.Id("cred_sign_in_button")).Click();
            Thread.Sleep(2000);
            Actions action = new Actions(driver);
            string id="";
            int line = 0 ;

            //every 5 test cases save
            for(int index=0; index<idList.Count; index++)
            {
                IWebElement firstCell;
                if (index == 0)
                {
                    driver.Url = "https://vmob.visualstudio.com/"+project+"/_testManagement?planId=" + testPlanId + "&suiteId=" + testSuiteId + "&_a=tests";
                    driver.Navigate();
                    Thread.Sleep(5000);
                    //new test case
                    IWebElement newButton = driver.FindElement(By.XPath("//*[@id=\"mi_113\"]/span[2]"));
                    newButton.Click();
                    Thread.Sleep(1000);
                    action.SendKeys(Keys.Down).SendKeys(Keys.ArrowDown).SendKeys(Keys.Enter);
                    action.Perform();
                    Thread.Sleep(5000);
                    //add test case
                    firstCell = driver.FindElement(By.ClassName("test-edit-grid-area")).FindElement(By.XPath("./div"));
                    id = firstCell.GetAttribute("id");
                }
                line++;
                string prefix = "//*[@id=\"row_" + id + "_" + line + "\"" + "]";
                Console.WriteLine("title line:{0}", prefix);
                IWebElement titleTab = driver.FindElement(By.XPath(prefix + "/div[2]"));
                line++;
                prefix = "//*[@id=\"row_" + id + "_" + line + "\"" + "]";
                Console.WriteLine("precondition line:{0}", prefix);
                IWebElement preconditionTab = driver.FindElement(By.XPath(prefix + "/div[3]"));
                line++; 
                prefix = "//*[@id=\"row_" + id + "_" + line + "\"" + "]";
                IWebElement actionTab = driver.FindElement(By.XPath(prefix + "/div[3]"));
                IWebElement resultTab = driver.FindElement(By.XPath(prefix + "/div[4]"));

                //input title
                string title = nameList[index];
                titleTab.Click();
                Thread.Sleep(1000);
                Console.WriteLine("{0}", title);
                action.SendKeys(title);
                action.Perform();
                Thread.Sleep(3000);

                //input prediction
                string precondition = preconditionList[index];
                preconditionTab.Click();
                Thread.Sleep(1000);
                action.SendKeys(precondition).Perform();
                Thread.Sleep(3000);

                //input action
                List<string> actions = stepActionList[index];
                List<string> results = stepResultList[index];
                for (int subindex = 0; subindex < actions.Count; subindex++)
                {
                    actionTab = driver.FindElement(By.XPath(prefix + "/div[3]"));
                    resultTab = driver.FindElement(By.XPath(prefix + "/div[4]"));
                    //stepAction input
                    string actionContext = actions[subindex];
                    //actionTab.Click();
                    action.SendKeys(Keys.Enter).Perform();
                    Thread.Sleep(1000);
                    action.SendKeys(actionContext);
                    action.Perform();
                    Thread.Sleep(3000);


                    //stepResult input
                    
                    string resultContext = results[subindex];
                    action.SendKeys(Keys.Tab).Perform();
                    Thread.Sleep(1000);
                    action.SendKeys(resultContext).SendKeys(Keys.Enter);
                    action.Perform();
                    Thread.Sleep(1000);
                    action.SendKeys(Keys.Escape).SendKeys(Keys.Up).SendKeys(Keys.Escape).SendKeys(Keys.ArrowLeft);
                    action.Perform();
                    Thread.Sleep(3000);
                    line++;
                    prefix = "//*[@id=\"row_" + id + "_" + line + "\"" + "]";
                }
                if (index % 5 == 0)
                {
                    action.KeyDown(Keys.Control).SendKeys("s").KeyUp(Keys.Control);
                    action.Perform();
                    Thread.Sleep(3000);
                    driver.Url = "https://vmob.visualstudio.com/"+project+"/_testManagement?planId=" + testPlanId + "&suiteId=" + testSuiteId + "&_a=tests";
                    driver.Navigate();
                    Thread.Sleep(5000);
                    //new test case
                    IWebElement newButton = driver.FindElement(By.XPath("//*[@id=\"mi_113\"]/span[2]"));
                    newButton.Click();
                    Thread.Sleep(1000);
                    action.SendKeys(Keys.Down).SendKeys(Keys.ArrowDown).SendKeys(Keys.Enter);
                    action.Perform();
                    Thread.Sleep(10000);
                    //add test case
                    firstCell = driver.FindElement(By.ClassName("test-edit-grid-area")).FindElement(By.XPath("./div"));
                    id = firstCell.GetAttribute("id");
                    line = 0;
                } 
            }
            action.KeyDown(Keys.Control).SendKeys("s").KeyUp(Keys.Control);
            action.Perform();
           Thread.Sleep(3000);




            //recover
            driver.Url = "https://vmob.testrail.net/index.php?/suites/view/" + testRailSuiteId;
            driver.Navigate();
            Thread.Sleep(3000);
            driver.FindElement(By.Id("filterCasesReset")).Click();

            driver.Quit();
        }

    }
}
