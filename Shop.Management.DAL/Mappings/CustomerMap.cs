using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Shop.Management.Model;

namespace Shop.Management.DAL.Mappings
{
    public class CustomerMap : ClassMapping<Customer>
    {
        public CustomerMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.GuidComb));
            Property(x => x.FirstName);
            Property(x => x.LastName);
            Property(x => x.Email);
            Property(x => x.Phone);
            Property(x => x.City);
            Property(x => x.Country);
            Property(x => x.PostalCode);
            Property(x => x.Street);

            Set(e => e.Orders,
                mapper => { mapper.Key(k => k.Column("CustomerId")); },
                relation => relation.OneToMany(mapping => mapping.Class(typeof (Order))));
        }
    }
}