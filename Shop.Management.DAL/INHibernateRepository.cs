using System;
using System.Collections.Generic;
using Shop.Management.Model;

namespace Shop.Management.DAL
{
    public interface INHibernateRepository<T> where T : class
    {
        void Save(T entity);
        T GetById(Guid id);
        void Update(T entity);
        void Delete(T entity);
        List<T> GetAll();
        void SaveOrUpdate(T entity);
    }
}