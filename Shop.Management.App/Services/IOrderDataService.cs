using System.Collections.Generic;
using Shop.Management.Model;

namespace Shop.Management.App.Services
{
    internal interface IOrderDataService
    {
        void SaveOrder(Order _order);
        List<Order> GetAllOrders();
    }
}