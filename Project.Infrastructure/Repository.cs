using Project.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Project.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly IDbContext _context;
        private IDbSet<T> _dbSet;

        public Repository(IDbContext context)
        {
            _context = context;
            _dbSet = context.GetDbSet<T>();
        }

        public T GetById(object Id)
        {
            return _dbSet.Find(Id);
        }

        public T GetFirst(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public T Insert(T Entity)
        {
            return _dbSet.Add(Entity);
        }

        public void Update(T Entity)
        {
            _dbSet.Attach(Entity);
            var entry = _context.Entry(Entity);
            entry.State = EntityState.Modified;
        }

        public void Delete(T Entity)
        {
            _dbSet.Remove(Entity);
        }
    }
}
