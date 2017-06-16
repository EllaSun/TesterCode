using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Plexure.Service.Security.IntegrationTest
{
    public class JsonReader
    {
        public List<T> ReadList<T>(string file)
        {
            List<T> data = new List<T>();
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                data = JsonConvert.DeserializeObject<List<T>>(json);
            }
            return data;
        }

        public T ReadSingleEntry<T>(string file)
        {
            T data = default(T);

            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                data = JsonConvert.DeserializeObject<T>(json);
            }

            return data;
        }
    }
}