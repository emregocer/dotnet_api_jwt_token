using Microsoft.AspNet.Identity;
using Project.Core.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Identity
{
    public interface IAppUserStore : IUserStore<AppUser, int>
    {

    }
}
