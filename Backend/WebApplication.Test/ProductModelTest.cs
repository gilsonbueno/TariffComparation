using WebApplication.Model;
using Xunit;

namespace WebApplication.Test
{
    public class ProductModelTest
    {
        [Fact]
        public void ValidPackageProduct()
        {
            var product = CreateValidProduct();
            Assert.True(product.IsValid());
        }

        [Fact]
        public void ValidBasicProduct()
        {
            var product = CreateValidProduct();
            product.ConsumeType = ConsumeTypeModel.Basic;

            Assert.True(product.IsValid());
        }

        [Fact]
        public void ProductTypeUndefined()
        {
            var product = CreateValidProduct();
            product.ConsumeType = ConsumeTypeModel.Undefined;

            Assert.False(product.IsValid());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ProductPriceKwhInvalid(double price)
        {
            var product = CreateValidProduct();
            product.PriceKwh = price;

            Assert.False(product.IsValid());
        }

        [Fact]
        public void ProductNameInvalid()
        {
            var product = CreateValidProduct();
            product.Name = string.Empty;

            Assert.False(product.IsValid());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ProductBasicPricePeriod(double price)
        {
            var product = CreateValidProduct();
            product.ConsumeType = ConsumeTypeModel.Basic;
            product.PricePeriod = price;

            Assert.False(product.IsValid());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ProductPackageLimitPackage(int limit)
        {
            var product = CreateValidProduct();
            product.LimitPackage = limit;

            Assert.False(product.IsValid());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ProductPackagePricePackage(int price)
        {
            var product = CreateValidProduct();
            product.PricePackage = price;

            Assert.False(product.IsValid());
        }

        private ProductModel CreateValidProduct()
        {
            return new ProductModel
            {
                ConsumeType = ConsumeTypeModel.Package,
                Id = 1,
                Name = "Test",
                LimitPackage = 10,
                PriceKwh = 50,
                PricePackage = 123,
                PricePeriod = 456
            };
        }
    }
}
