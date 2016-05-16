using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Shop.Management.Model;

namespace Shop.Management.DAL.Mappings
{
    public class OrderItemMap : ClassMapping<OrderItem>
    {
        public OrderItemMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.GuidComb));
            Property(x => x.Price);
            Property(x => x.Quantity);

            ManyToOne(x => x.Order, manyToOne =>
            {
                manyToOne.Column("OrderId");
                manyToOne.Cascade(Cascade.None);
                manyToOne.NotNullable(true);
            });

            ManyToOne(x => x.Product, manyToOne =>
            {
                manyToOne.Column("ProductId");
                manyToOne.Cascade(Cascade.None);
                manyToOne.NotNullable(true);
            });
        }
    }
}