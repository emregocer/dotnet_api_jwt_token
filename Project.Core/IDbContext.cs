using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Project.Core
{
    public interface IDbContext : IDisposable
    {
        int SaveChanges();
        IDbSet<T> GetDbSet<T>() where T : class, IEntity;
        DbEntityEntry Entry(object entity);
        DbEntityEntry<T> Entry<T>(T entity) where T : class;
    }
}
