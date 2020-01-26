using System.Collections.Generic;
using System.Linq;
using WebApplication.Business.Interface;
using WebApplication.Model;
using WebApplication.Repository;

namespace WebApplication.Business
{
    public class TariffBusiness : ITariffBusiness
    {
        private readonly ProductRepository productRepository;

        public TariffBusiness(ProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public List<ConsumeModel> Compare(int consumption)
        {
            var products = productRepository.GetAll();

            var resultList = new List<ConsumeModel>();

            foreach (var product in products)
            {
                var consumed = Calcule(product, consumption);
                resultList.Add(consumed);
            }

            return resultList.OrderBy(t => t.AnnualCost).ToList();
        }

        public ConsumeModel Calcule(ProductModel product, int consume)
        {
            var result = new ConsumeModel
            {
                Name = product.Name,
            };

            result.AnnualCost = product.ConsumeType == ConsumeTypeModel.Basic ? BasicCalculate(product, consume) : PackageCalculate(product, consume);

            return result;
        }

        public virtual double BasicCalculate(ProductModel product, int consumption)
        {
            return (product.PricePeriod * 12 ) + (consumption * product.PriceKwh);
        }

        public virtual double PackageCalculate(ProductModel product, int consumption)
        {
            if (consumption < product.LimitPackage)
            {
                return product.PricePackage;
            }

            return product.PricePackage + ((consumption - product.LimitPackage) * product.PriceKwh);
        }
    }
}
