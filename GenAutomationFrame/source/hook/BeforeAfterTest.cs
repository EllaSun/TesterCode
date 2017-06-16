using TechTalk.SpecFlow;

namespace Plexure.Service.Security.IntegrationTest
{
    [Binding]
    public sealed class BeforeAfterTest
    {
        [BeforeTestRun]
        public static void BeforeTest()
        {
            CommonConfig config = new CommonConfig();
            config.getUrl();
            SuffixGen.WriteRequestSuffix();
        }
    }
}