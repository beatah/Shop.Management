using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;

namespace Shop.Management.DAL
{
    public class NHibernateRepository<T> : INHibernateRepository<T> where T : class
    {
        public void Save(T entity)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(entity);
                transaction.Commit();
            }
        }

        public T GetById(Guid id)
        {
            using (var session = NHibernateHelper.OpenSession())
                return session.Get<T>(id);
        }

        public List<T> GetAll()
        {
            using (var session = NHibernateHelper.OpenSession())
                return session.Query<T>().ToList();
        }

        public void SaveOrUpdate(T entity)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(entity);
                transaction.Commit();
            }
        }


        public void Update(T entity)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Update(entity);
                transaction.Commit();
            }
        }

        public void Delete(T entity)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Delete(entity);
                transaction.Commit();
            }
        }
    }
}