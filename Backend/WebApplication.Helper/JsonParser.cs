using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace WebApplication.Helper
{
    public class JsonParser<T> where T : class
    {
        public List<T> GetObjectsByJson(string filePath)
        {
            var result = new List<T>();
            using (StreamReader file = File.OpenText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                result = (List<T>)serializer.Deserialize(file, typeof(List<T>));
            }

            return result;
        }
    }
}
