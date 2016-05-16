using System;

namespace Shop.Management.Model
{
    public class OrderItem
    {
        private Order _order;

        public OrderItem(Order order, Product product, int quantity)
        {
            Order = order;
            Product = product;
            Price = product.Price;
            Quantity = quantity;
        }

        public OrderItem()
        {
        }

        public virtual Guid Id { get; set; }
        public virtual decimal Price { get; set; }
        public virtual Product Product { get; set; }
        public virtual int Quantity { get; set; }

        public virtual Order Order { get; set; }

        public virtual decimal TotalPrice()
        {
            return Quantity*Price;
        }
    }
}