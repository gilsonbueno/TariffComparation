using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using WebApplication.Model;

namespace WebApplication.Helper
{
    public class CsvParser<T> where T : class
    {
        public List<T> GetObjectsByCsv(string filePath)
        {
            var result = new List<T>();
            TextReader reader = new StreamReader(filePath);
            using (var csv = new CsvReader(reader, GetConfiguration()))
            {
                csv.Configuration.TypeConverterOptionsCache.GetOptions(typeof(double)).NumberStyle = NumberStyles.AllowDecimalPoint;
                result = csv.GetRecords<T>().ToList();
            }
            return result;
        }

        private Configuration GetConfiguration()
        {
            var configuration = new Configuration
            {
                Delimiter = ",",
                CultureInfo = CultureInfo.GetCultureInfo("en-GB")
            };

            if (this.GetType() == typeof(CsvParser<ProductModel>))
            {
                configuration.RegisterClassMap<ProductMapper>();
            }

            return configuration;
        }
    }
}
