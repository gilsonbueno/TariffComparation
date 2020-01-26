using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Business.Interface;
using WebApplication.Model;
using WebApplication.Repository;

namespace WebApplication.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly ProductRepository productRepository;

        public ProductBusiness(ProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ProductModel Create(ProductModel product)
        {
            if (!product.IsValid())
            {
                throw new Exception("Bad Request");
            }

            var result = productRepository.Create(product);
            return result;
        }

        public void Delete(int id)
        {
            productRepository.Delete(id);
        }

        public List<ProductModel> GetAll()
        {
            var products = productRepository.GetAll();
            return products.ToList();
        }

        public ProductModel GetById(int id)
        {
            var product = productRepository.GetById(id);
            return product;
        }

        public ProductModel Update(ProductModel product, int id)
        {
            if (!product.IsValid())
            {
                throw new Exception("Bad Request");
            }

            return productRepository.Update(product, id);
        }
    }
}
