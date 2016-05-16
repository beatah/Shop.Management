using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using Shop.Management.DAL.Mappings;

namespace Shop.Management.DAL
{
    public static class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static Configuration _configuration;
        private static HbmMapping _mapping;

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = Configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static Configuration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = CreateConfiguration();
                }
                return _configuration;
            }
        }

        public static HbmMapping Mapping
        {
            get
            {
                if (_mapping == null)
                {
                    _mapping = CreateMapping();
                }
                return _mapping;
            }
        }


        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        private static Configuration CreateConfiguration()
        {
            var configuration = new Configuration();

            configuration.Configure();

            configuration.AddDeserializedMapping(Mapping, null);
            //Creates database structure
           // new SchemaExport(configuration).Execute(true,true,false);
            return configuration;
        }

        private static HbmMapping CreateMapping()
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(new List<Type> {typeof (OrderMap)});
            mapper.AddMappings(new List<Type> {typeof (OrderItemMap)});
            mapper.AddMappings(new List<Type> {typeof (ProductMap)});
            mapper.AddMappings(new List<Type> {typeof (EmployeeMap)});
            mapper.AddMappings(new List<Type> {typeof (CustomerMap)});

            return mapper.CompileMappingForAllExplicitlyAddedEntities();
        }
    }
}