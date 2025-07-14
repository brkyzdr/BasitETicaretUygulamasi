using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        List<T> Find(Expression<Func<T, bool>> filter);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        void Save();
    }
}
