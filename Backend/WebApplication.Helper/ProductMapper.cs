using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Globalization;
using WebApplication.Model;

namespace WebApplication.Helper
{
    public class ProductMapper : ClassMap<ProductModel>
    {
        public ProductMapper()
        {
            Map(m => m.Id).Name("Id").TypeConverter<IntConverter>();
            Map(m => m.Name).Name("Name");
            Map(m => m.LimitPackage).Name("LimitPackage").TypeConverter<IntConverter>();
            Map(m => m.PriceKwh).Name("PriceKwh").TypeConverter<DoubleConverter>();
            Map(m => m.PricePackage).Name("PricePackage").TypeConverter<DoubleConverter>();
            Map(m => m.PricePeriod).Name("PricePeriod").TypeConverter<DoubleConverter>();
            Map(m => m.ConsumeType).Name("ConsumeType").TypeConverter<ConsumeTypeConverter>();
        }
    }

    public class IntConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (text == null)
            {
                return 0;
            }
            CultureInfo culture = new CultureInfo("en-US");
            var intValue = Convert.ToInt32(text, culture);

            return intValue;
        }
    }

    public class DoubleConverter: DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (text == null)
            {
                return 0;
            }
            CultureInfo culture = new CultureInfo("en-US");
            var doubleValue = Convert.ToDouble(text, culture);

            return doubleValue;
        }
    }

    public class ConsumeTypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            switch (text)
            {
                case "1": return ConsumeTypeModel.Basic;
                case "2": return ConsumeTypeModel.Package;
                default: return ConsumeTypeModel.Undefined;
            }
        }
    }
}