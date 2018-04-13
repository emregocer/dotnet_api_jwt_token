using Microsoft.AspNet.Identity.EntityFramework;
using Project.Core;
using Project.Core.Model;
using Project.Core.IdentityModel;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web;

namespace Project.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>, IDbContext
    {
        public AppDbContext() : base("RDB")
        {
        }

        public DbSet<Card> Cards { get; set; }

        public IDbSet<T> GetDbSet<T>() where T : class, IEntity
        {
            return Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
               
        public override int SaveChanges()
        {
            var currentUsername = !string.IsNullOrEmpty(HttpContext.Current?.User?.Identity?.Name)
            ? HttpContext.Current.User.Identity.Name
            : "Anonymous";

            foreach (var entry in ChangeTracker.Entries())
            {
                var entity = entry.Entity;

                var t = entity.GetType();

                if (t.GetProperty("Id") == null)
                    continue;

                var entityType = t.GetProperty("Id").PropertyType;

                if (entry.State == EntityState.Added)
                {
                    if(entityType == typeof(int))
                    {
                        ((Entity<int>)entity).CreatedTime = DateTime.Now;
                    }
                    else if (entityType == typeof(string))
                    {
                        ((Entity<string>)entity).CreatedTime = DateTime.Now;
                    }
                    else
                    {
                        ((Entity<Guid>)entity).CreatedTime = DateTime.Now;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entityType == typeof(int))
                    {
                        ((Entity<int>)entity).ModifiedTime = DateTime.Now;
                    }
                    else if (entityType == typeof(string))
                    {
                        ((Entity<string>)entity).ModifiedTime = DateTime.Now;
                    }
                    else
                    {
                        ((Entity<Guid>)entity).ModifiedTime = DateTime.Now;
                    }
                }
            }
            return base.SaveChanges();
        }       
    }
}
