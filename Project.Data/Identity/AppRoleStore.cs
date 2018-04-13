using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Project.Core.Identity;
using Project.Core.IdentityModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data
{
    public class AppRoleStore : RoleStore<AppRole, int, AppUserRole>, IAppRoleStore
    {
        public AppRoleStore() : base(new AppDbContext())
        {

        }

        public AppRoleStore(DbContext context) : base(context)
        {
        }
    }
}
