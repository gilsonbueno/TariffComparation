using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Business;
using WebApplication.Model;
using WebApplication.Repository;
using Xunit;

namespace WebApplication.Test
{
    public class TariffBusinessTest
    {
        [Theory]
        [InlineData(3500, 830)]
        [InlineData(4500, 1050)]
        [InlineData(6000, 1380)]
        public void BasicCalc(int consumption, double expected)
        {
            var product = new ProductModel
            {
                ConsumeType = ConsumeTypeModel.Basic,
                PriceKwh = 0.22,
                PricePeriod = 5
            };

            var tariffBusiness = new TariffBusiness(new ProductRepository());
            var result = tariffBusiness.BasicCalculate(product, consumption);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(3500, 800)]
        [InlineData(4500, 950)]
        [InlineData(6000, 1400)]
        public void PackageCalc(int consumption, double expected)
        {
            var product = new ProductModel
            {
                ConsumeType = ConsumeTypeModel.Package,
                PriceKwh = 0.30,
                PricePeriod = 0,
                LimitPackage = 4000,
                PricePackage = 800
            };

            var tariffBusiness = new TariffBusiness(new ProductRepository());
            var result = tariffBusiness.PackageCalculate(product, consumption);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(3500, 800)]
        [InlineData(4500, 950)]
        [InlineData(6000, 1400)]
        public void CalculeBasicType(int consumption, double expected)
        {
            var product = new ProductModel
            {
                ConsumeType = ConsumeTypeModel.Basic,
                Name = "Test"
            };
            var tariffMock = new Mock<TariffBusiness>(new ProductRepository()) { CallBase = true };
            tariffMock.Setup(t => t.BasicCalculate(It.IsAny<ProductModel>(), It.IsAny<int>())).Returns(expected);

            var tariffBusiness = tariffMock.Object;

            var result = tariffBusiness.Calcule(product, consumption);
            Assert.Equal(expected, result.AnnualCost);
            Assert.Equal("Test", result.Name);
            tariffMock.Verify(t => t.BasicCalculate(It.IsAny<ProductModel>(), It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [InlineData(3500, 800)]
        [InlineData(4500, 950)]
        [InlineData(6000, 1400)]
        public void CalculePackageType(int consumption, double expected)
        {
            var product = new ProductModel
            {
                ConsumeType = ConsumeTypeModel.Package,
                Name = "Test"
            };
            var tariffMock = new Mock<TariffBusiness>(new ProductRepository()) { CallBase = true };
            tariffMock.Setup(t => t.PackageCalculate(It.IsAny<ProductModel>(), It.IsAny<int>())).Returns(expected);

            var tariffBusiness = tariffMock.Object;

            var result = tariffBusiness.Calcule(product, consumption);
            Assert.Equal(expected, result.AnnualCost);
            Assert.Equal("Test", result.Name);
            tariffMock.Verify(t => t.PackageCalculate(It.IsAny<ProductModel>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Compare()
        {
            var products = new List<ProductModel>
            {
                new ProductModel
                {
                    Name = "product 1",
                    ConsumeType = ConsumeTypeModel.Package,
                    PriceKwh = 0.30,
                    PricePeriod = 0,
                    LimitPackage = 4000,
                    PricePackage = 800
                },
                new ProductModel
                {
                    Name = "product 2",
                    ConsumeType = ConsumeTypeModel.Basic,
                    PriceKwh = 0.22,
                    PricePeriod = 5
                }
            };

            var repository = new Mock<ProductRepository>();
            repository.Setup(r => r.GetAll()).Returns(products);
            var tariffMock = new Mock<TariffBusiness>(repository.Object) { CallBase = true };
            tariffMock.Setup(t => t.PackageCalculate(It.IsAny<ProductModel>(), It.IsAny<int>())).Returns(850);
            tariffMock.Setup(t => t.BasicCalculate(It.IsAny<ProductModel>(), It.IsAny<int>())).Returns(900);

            var tariffBusiness = tariffMock.Object;

            var result = tariffBusiness.Compare(10);

            Assert.Equal(2, result.Count);
            Assert.Equal(850, result.FirstOrDefault().AnnualCost);
            Assert.Equal("product 1", result.FirstOrDefault().Name);

            Assert.Equal(900, result.LastOrDefault().AnnualCost);
            Assert.Equal("product 2", result.LastOrDefault().Name);
        }
    }
}
