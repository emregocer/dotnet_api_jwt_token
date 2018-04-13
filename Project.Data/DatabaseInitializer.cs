using Microsoft.AspNet.Identity;
using Project.Core.IdentityModel;
using Project.Core.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Project.Data
{
    public class DatabaseInitializer<T> : DropCreateDatabaseAlways<AppDbContext>
    {
        public override void InitializeDatabase(AppDbContext context)
        {
            base.InitializeDatabase(context);
        }

        protected override void Seed(AppDbContext context)
        {
            base.Seed(context);
            // Create a role.
            if (!context.Roles.Any(r => r.Name == "Mod"))
            {
                var store = new AppRoleStore(context);
                var manager = new AppRoleManager(store);
                var role = new AppRole { Name = "Mod" };

                manager.Create(role);
                context.SaveChanges();
            }

            // Create a user.
            if (!context.Users.Any(u => u.UserName == "default"))
            {
                var store = new AppUserStore(context);
                var manager = new AppUserManager(store);
                var user = new AppUser { UserName = "default" };

                manager.Create(user, "123456");
                manager.AddToRole(user.Id, "Mod");
                context.SaveChanges();
            }

            var cards = new List<Card>
            {
                new Card { Name = "Card Name 1"},
                new Card { Name = "Card Name 2"},
                new Card { Name = "Card Name 3"},
                new Card { Name = "Card Name 4"}
            };

            cards.ForEach(c => context.Cards.Add(c));
            context.SaveChanges();
        }
    }
}
