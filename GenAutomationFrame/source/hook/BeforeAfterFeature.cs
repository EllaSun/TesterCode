using TechTalk.SpecFlow;

namespace Plexure.Service.Security.IntegrationTest
{
    [Binding]
    public sealed class BeforeAfterFeature
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [BeforeFeature]
        public static void BeforeFeature()
        {
            string token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0ZGI5MzQ0YS0zOTNiLTRmOTUtOWM2OC1lYzY1NjZhMzJiNGYiLCJjaWQiOiI0ZGI5MzQ0YS0zOTNiLTRmOTUtOWM2OC1lYzY1NjZhMzJiNGYiLCJncmFudF90eXBlIjoiY2xpZW50X2NyZWRlbnRpYWxzIiwic2NvcGUiOiJ7XCJUZW5hbnRSb2xlc1wiOlt7XCJSb2xlc1wiOltcInRlbmFudC1hZG1pbmlzdHJhdG9yXCIsXCJ0ZW5hbnQtdXNlclwiXSxcIlRlbmFudElkXCI6XCJkNjQyNWYxMi01Zjc2LTRiN2UtYjk1Ny0xOWJmYmRkY2NhMGJcIn1dLFwiUGxhdGZvcm1Sb2xlc1wiOntcIlJvbGVzXCI6W1wicGxhdGZvcm0tYWRtaW5pc3RyYXRvclwiLFwicGxhdGZvcm0tdXNlclwiXX19IiwiYWNjb3VudF9wcm9maWxlIjp7IkFjY291bnRJZCI6IjRkYjkzNDRhLTM5M2ItNGY5NS05YzY4LWVjNjU2NmEzMmI0ZiIsIkZhbWlseU5hbWUiOiJ3b3JrZmxvd19hZG1pbkBwbGV4dXJlLmNvbSIsIkdpdmVuTmFtZSI6IndvcmtmbG93X2FkbWluQHBsZXh1cmUuY29tIiwiT0F1dGhTZWNyZXQiOiJ6ZW1wbkRKcTZFK29aR0g2a1NMQ3FRPT0iLCJSb2xlIjoicGxhdGZvcm0tYWRtaW5pc3RyYXRvciJ9LCJpYXQiOiIxNDgwNDU5MTcxIiwic3hwIjoiMTUxMTk5NTE3MSIsImVzY3J0IjoiaWVXME03b21sQVlMUk12eTVuS1IiLCJhdWQiOlsiUGxleHVyZS1EZXYtMSIsInRlbmFudC1kNjQyNWYxMi01Zjc2LTRiN2UtYjk1Ny0xOWJmYmRkY2NhMGItdGVuYW50LWFkbWluaXN0cmF0b3IiXSwibmJmIjoxNDgwNDU5MTQxLCJleHAiOjE1MTE5OTUxNzEsImlzcyI6InBsZXh1cmUuY29tIn0.reYK8JU1in4e6e7nDNlufpqCsaBjt_WaiP6BxS2Ai0U";
            FeatureContext.Current["token"] = token;
        }
    }
}