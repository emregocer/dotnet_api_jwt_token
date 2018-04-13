using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Project.Core
{
    public interface IRepository<T> where T : class, IEntity
    {
        T Insert(T Entity);
        void Delete(T Entity);
        void Update(T Entity);
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        T GetById(object Id);
        T GetFirst(Expression<Func<T, bool>> predicate);
    }
}
