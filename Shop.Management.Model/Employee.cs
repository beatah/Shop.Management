using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Management.Model
{
    public class Employee
    {
        public Employee()
        {
            Orders = new List<Order>();
        }

        public virtual Guid Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string Phone { get; set; }
        public virtual DateTime HireDate { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public int NumberOfOrders
        {
            get { return Orders.Count; }
        }

        public virtual string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public decimal Commision
        {
            get { return 0.1M*Orders.Sum(order => order.TotalPrice); }
        }
    }
}