using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Management.Model
{
    public class Order
    {
        private Customer _customer;
        private Employee _employee;
        private ICollection<OrderItem> _items;
        private DateTime _orderDate;

        public Order()
        {
            _items = new List<OrderItem>();
        }

        public Order(Employee employee, Customer customer)
        {
            _employee = employee;
            _customer = customer;
            _orderDate = DateTime.Now;
            _items = new List<OrderItem>();
            _employee.Orders.Add(this);
            _customer.Orders.Add(this);
        }

        public virtual decimal TotalPrice
        {
            get { return Items.Sum(orderItem => orderItem.TotalPrice()); }
        }

        public virtual Guid Id { get; set; }

        public virtual Employee Employee
        {
            get { return _employee; }
            set { _employee = value; }
        }

        public virtual Customer Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }

        public virtual DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }

        public virtual ICollection<OrderItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public virtual void AddItem(Product product, int quantity)
        {
            product.InStock -= quantity;
            _items.Add(new OrderItem(this, product, quantity));
        }
    }
}