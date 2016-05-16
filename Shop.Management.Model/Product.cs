using System;
using System.Collections.Generic;

namespace Shop.Management.Model
{
    public class Product
    {
        public Product()
        {
            Orders = new List<OrderItem>();
        }

        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal Price { get; set; }
        public virtual int InStock { get; set; }
        public virtual ICollection<OrderItem> Orders { get; set; }
    }
}