using Newtonsoft.Json;

namespace Plexure.Service.Security.IntegrationTest
{
    public class JsonWriter
    {
        public void Write(string output, object data)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            System.IO.File.WriteAllText(output, json);
        }
    }
}