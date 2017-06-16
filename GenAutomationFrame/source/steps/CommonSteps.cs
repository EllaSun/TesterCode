using NUnit.Framework;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Plexure.Service.Security.IntegrationTest
{
    [Binding]
    public sealed class CommonSteps
    {
        [Then(@"the response code should be (.*)")]
        public void ThenTheResponseCodeShouldBe(int statusCode)
        {
            int getStatusCode = ScenarioContext.Current.Get<int>("statusCode");
            Assert.AreEqual(statusCode, getStatusCode);
        }

        [Then(@"the response body (\w*) should be (.*)")]
        public void ThenTheResponseBodyShouldBe(string field, string value)
        {
            
            var response = JsonConvert.SerializeObject(ScenarioContext.Current["Response"]);
            var data = JObject.Parse(response).SelectToken(field);
            Assert.False(data.HasValues);
            string expect = value;
            if (value == "empty")
                expect = string.Empty;
            Assert.AreEqual(expect, data.ToString());
        }


        [Then(@"the response body (\w*) (\w*) should be (.*)")]
        public void ThenTheResponseBodyTenantsRoleShouldBe(string sub, string field, string value)
        {
            var response = JsonConvert.SerializeObject(ScenarioContext.Current["Response"]);
            var data = JObject.Parse(response).SelectToken(sub+"[0]."+field);
            Assert.False(data.HasValues);
            string expect = value;
            if (value == "empty")
                expect = string.Empty;
            Assert.AreEqual(expect, data.ToString());
        }


    }
}