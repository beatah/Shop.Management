using System.Collections.Generic;
using Shop.Management.DAL;
using Shop.Management.Model;

namespace Shop.Management.App.Services
{
    internal class ProductDataService : IProductDataService
    {
        private readonly INHibernateRepository<Product> _repository = new NHibernateRepository<Product>();

        public List<Product> GetAllProducts()
        {
            return _repository.GetAll();
        }

        public void SaveProduct(Product product)
        {
            _repository.SaveOrUpdate(product);
        }
    }

}