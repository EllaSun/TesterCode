using System;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace Plexure.Service.Security.IntegrationTest
{
    [Binding]
    public sealed class AfterSteps
    {
        [AfterStep]
        public void AfterStep()
        {
            if (ScenarioContext.Current.TestError == null)
            {
                if (ScenarioContext.Current.ContainsKey("steps") == false) ScenarioContext.Current["steps"] = ScenarioContext.Current.CurrentScenarioBlock +" " + ScenarioStepContext.Current.StepInfo.Text+"->success";
                else ScenarioContext.Current["steps"] =   ScenarioContext.Current["steps"] + "\r\n" + ScenarioContext.Current.CurrentScenarioBlock + " " + ScenarioStepContext.Current.StepInfo.Text+"->success";
            }
            else
            {
                Assert.Fail("\r\n"+ScenarioContext.Current["steps"]+"\r\n"+ScenarioContext.Current.CurrentScenarioBlock +" "+ScenarioStepContext.Current.StepInfo.Text + "->fail!\r\n" + ScenarioContext.Current.TestError.Message);
            }
        }
    }
}