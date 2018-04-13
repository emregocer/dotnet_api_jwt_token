using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Project.Core.Identity;
using Project.Core.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data
{
    public class AppUserStore :
        UserStore<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>,
        IAppUserStore
    {
        public AppUserStore() : base(new AppDbContext())
        {

        }

        public AppUserStore(AppDbContext context) : base(context)
        {

        }
    }

}
