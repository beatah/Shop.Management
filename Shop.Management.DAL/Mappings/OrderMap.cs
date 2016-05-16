using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Shop.Management.Model;

namespace Shop.Management.DAL.Mappings
{
    public class OrderMap : ClassMapping<Order>
    {
        public OrderMap()
        {
            Table("[Order]");
            Id(x => x.Id, m => m.Generator(Generators.GuidComb));
            Property(x => x.OrderDate);


            ManyToOne(b => b.Customer,
                mapper =>
                {
                    mapper.Column("CustomerId");
                    mapper.Cascade(Cascade.All);
                    mapper.Class(typeof (Customer));
                });

            ManyToOne(b => b.Employee,
                mapper =>
                {
                    mapper.Column("EmployeeId");
                    mapper.Cascade(Cascade.All);
                    mapper.Class(typeof (Employee));
                });
            Set(e => e.Items,
                mapper =>
                {
                    mapper.Key(k => k.Column("OrderId"));
                    mapper.Cascade(Cascade.All);
                    mapper.Inverse(true);
                },
                relation => relation.OneToMany(mapping => mapping.Class(typeof (OrderItem))));
        }
    }
}