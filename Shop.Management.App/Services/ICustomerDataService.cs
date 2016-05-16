using System.Collections.Generic;
using Shop.Management.Model;

namespace Shop.Management.App.Services
{
    internal interface ICustomerDataService
    {
        void DeleteCustomer(Customer customer);
        List<Customer> GetAll();
        void SaveCustomer(Customer customer);
        List<Customer> GetAllCustomersOrders();
    }
}