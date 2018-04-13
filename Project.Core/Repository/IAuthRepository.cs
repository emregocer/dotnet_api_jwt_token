using Project.Core.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core
{
    public interface IAuthRepository
    {
        Task<AppUser> IsRegistered(string username, string password);
    }
}
