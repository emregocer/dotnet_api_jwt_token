using Microsoft.AspNet.Identity;
using Project.Core.Identity;
using Project.Core.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data
{
    public class AppUserManager : UserManager<AppUser, int>
    {
        public AppUserManager(IAppUserStore store) : base(store)
        {

        }
    }

}
