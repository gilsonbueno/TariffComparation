using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Model;

namespace WebApplication.Business.Interface
{
    public interface IProductBusiness
    {
        List<ProductModel> GetAll();
        ProductModel Create(ProductModel product);
        ProductModel GetById(int id);
        ProductModel Update(ProductModel product, int id);
        void Delete(int id);
    }
}
