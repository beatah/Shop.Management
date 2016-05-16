using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Shop.Management.Model;

namespace Shop.Management.DAL.Mappings
{
    public class EmployeeMap : ClassMapping<Employee>
    {
        public EmployeeMap()
        {
            Lazy(false);
            Id(x => x.Id, m => m.Generator(Generators.GuidComb));
            Property(x => x.FirstName);
            Property(x => x.Username);
            Property(x => x.Password);
            Property(x => x.LastName);
            Property(x => x.Email);
            Property(x => x.Phone);
            Property(x => x.HireDate);

            Set(e => e.Orders,
                mapper => { mapper.Key(k => k.Column("EmployeeId")); },
                relation => relation.OneToMany(mapping => mapping.Class(typeof (Order))));
        }
    }
}