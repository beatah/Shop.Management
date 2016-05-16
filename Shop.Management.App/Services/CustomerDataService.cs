using System.Collections.Generic;
using System.Linq;
using Shop.Management.DAL;
using Shop.Management.Model;

namespace Shop.Management.App.Services
{
    internal class CustomerDataService : ICustomerDataService
    {
        private readonly INHibernateRepository<Customer> _repository = new NHibernateRepository<Customer>();

        public void DeleteCustomer(Customer customer)
        {
            _repository.Delete(customer);
        }

        public List<Customer> GetAll()
        {
            return _repository.GetAll();
        }

        public void SaveCustomer(Customer customer)
        {
            _repository.SaveOrUpdate(customer);
        }

        public List<Customer> GetAllCustomersOrders()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                Order order = null;
                Product product = null;
                OrderItem oi = null;

                var query = session.QueryOver<Customer>().Future();
                session.QueryOver<Customer>()
                    .Left.JoinAlias(x => x.Orders, () => order)
                    .Left.JoinAlias(() => order.Items, () => oi)
                    .Left.JoinAlias(() => oi.Product, () => product).Future();
                return query.ToList();
            }
        }
    }
}