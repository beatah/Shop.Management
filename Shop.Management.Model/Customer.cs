using System;
using System.Collections.Generic;

namespace Shop.Management.Model
{
    public class Customer
    {
        public Customer()
        {
            Orders = new List<Order>();
        }

        public virtual Guid Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Street { get; set; }
        public virtual string City { get; set; }
        public virtual int PostalCode { get; set; }
        public virtual string Country { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public virtual string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public virtual void AddOrder(Order order)
        {
            order.Customer = this;
            Orders.Add(order);
        }
    }
}