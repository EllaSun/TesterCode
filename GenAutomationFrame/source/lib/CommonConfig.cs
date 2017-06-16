using System.IO;

namespace Plexure.Service.Security.IntegrationTest
{
    internal class CommonConfig
    {
        static public string url;

        public void getUrl()
        {
            string path = Directory.GetCurrentDirectory();
            string file = File.ReadAllText(path + "/url.txt");
            url = file;
        }
    }
}