using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Shop.Management.Model;

namespace Shop.Management.DAL.Mappings
{
    public class ProductMap : ClassMapping<Product>
    {
        public ProductMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.GuidComb));
            Property(x => x.Name);
            Property(x => x.InStock);
            Property(x => x.Price);
            Bag(e => e.Orders,
                mapper =>
                {
                    mapper.Key(k => k.Column("ProductId"));
                    mapper.Inverse(true);
                },
                relation => relation.OneToMany(mapping => mapping.Class(typeof (OrderItem))));
        }
    }
}