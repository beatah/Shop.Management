using System.Collections.Generic;
using System.Linq;
using Shop.Management.DAL;
using Shop.Management.Model;

namespace Shop.Management.App.Services
{
    internal class EmployeeDataService : IEmployeeDataService
    {
        private readonly INHibernateRepository<Employee> _repository = new NHibernateRepository<Employee>();

        public void SaveEmployee(Employee employee)
        {
            _repository.SaveOrUpdate(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            _repository.Delete(employee);
        }

        public List<Employee> GetAllEmployees()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                Order order = null;
                OrderItem oi = null;

                var query = session.QueryOver<Employee>().Future();
                session.QueryOver<Employee>()
                    .Left.JoinAlias(x => x.Orders, () => order)
                    .Left.JoinAlias(() => order.Items, () => oi).Future();
                return query.ToList();
            }
        }

        public List<Employee> GetAll()
        {
            return _repository.GetAll();
        }
    }
}