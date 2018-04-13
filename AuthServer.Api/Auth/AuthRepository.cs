using Project.Core;
using Project.Core.Identity;
using Project.Core.IdentityModel;
using Project.Data;
using System.Threading.Tasks;

namespace AuthServer.Api.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IAppUserStore _userStore;
        // you can wrap this?
        private readonly AppUserManager _userManager;

        public AuthRepository(IAppUserStore userStore)
        {
            _userStore = userStore;
            _userManager = new AppUserManager(_userStore); // a better method for this?
        }

        public async Task<AppUser> IsRegistered(string username, string password)
        {
            return await _userManager.FindAsync(username, password);
        }
    }
}