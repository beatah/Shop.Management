using System.Collections.Generic;
using Shop.Management.Model;

namespace Shop.Management.App.Services
{
    internal interface IProductDataService
    {
        List<Product> GetAllProducts();
        void SaveProduct(Product product);
    }
}