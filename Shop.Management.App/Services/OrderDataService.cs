using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Management.DAL;
using Shop.Management.Model;

namespace Shop.Management.App.Services
{
    internal class OrderDataService : IOrderDataService
    {
        public void SaveOrder(Order _order)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var e1 = session.QueryOver<Employee>().Where(x => x.Id == _order.Employee.Id).SingleOrDefault();
                var c1 = session.QueryOver<Customer>().Where(x => x.Id == _order.Customer.Id).SingleOrDefault();
                var order = new Order(e1, c1);
                order.OrderDate = DateTime.Now;
                foreach (var orderitem in _order.Items)
                {
                    var p = session.QueryOver<Product>().Where(x => x.Id == orderitem.Product.Id).SingleOrDefault();
                    order.AddItem(p, orderitem.Quantity);
                }
                session.SaveOrUpdate(order);
                transaction.Commit();
            }
        }

        public List<Order> GetAllOrders()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                Product product = null;
                OrderItem oi = null;

                var query = session.QueryOver<Order>().Fetch(x => x.Customer).Eager.Future();
                session.QueryOver<Order>()
                    .Left.JoinAlias(x => x.Items, () => oi)
                    .Left.JoinAlias(() => oi.Product, () => product).Future();
                return query.ToList();
            }
        }
    }
}