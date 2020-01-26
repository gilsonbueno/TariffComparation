using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebApplication.Helper;
using WebApplication.Model;

namespace WebApplication.Repository
{
    public class ProductRepository : BaseRepository<ProductModel>
    {
        private IEnumerable<ProductModel> dataset;
        private int Id;
        
        public ProductRepository() : base()
        {
            if (dataset == null)
            {
                dataset = LoadData();
                Id = dataset.Count() + 1;
            }
        }

        public virtual IEnumerable<ProductModel> GetAll()
        {
            return dataset.ToArray();
        }

        public virtual ProductModel Create(ProductModel product)
        {
            Id += 1;
            product.Id = Id;
            dataset = dataset.Concat(new[] { product });
            return product;
        }

        public virtual ProductModel Update(ProductModel product, int id)
        {
            this.Delete(id);
            product.Id = id;
            dataset = dataset.Concat(new[] { product });
            return product;
        }

        public virtual void Delete(int id)
        {
            dataset = dataset.Where(d => d.Id != id);
        }

        public IEnumerable<ProductModel> LoadData()
        {
            //load data from Json file
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data\\products.json");
            var jsonProductParser = new JsonParser<ProductModel>();
            return jsonProductParser.GetObjectsByJson(path);

            //Use the lines below to load data from csv file
            //var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data\\products.csv");
            //var csvProductParser = new CsvParser<ProductModel>();
            //return csvProductParser.GetObjectsByCsv(path);
        }

        public virtual ProductModel GetById(int id)
        {
            return dataset.FirstOrDefault(p => p.Id == id);
        }
    }
}
