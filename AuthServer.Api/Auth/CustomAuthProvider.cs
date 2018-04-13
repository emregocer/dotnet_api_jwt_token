using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Project.Core;
using Project.Core.IdentityModel;
using Project.Data;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthServer.Api.Auth
{
    public class CustomAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly IAuthRepository _repo;

        public CustomAuthProvider()
        {
            _repo = new AuthRepository(new AppUserStore(new AppDbContext()));
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            string symmetricBase64Key = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "Client Id is not set.");
                return Task.FromResult<object>(null);
            }

            var audience = AudienceStore.GetTestAudience();

            if (context.ClientId != audience.ClientId)
            {
                context.SetError("invalid_clientId", string.Format("Client Id '{0}' is not valid.", context.ClientId));
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            AppUser user = await _repo.IsRegistered(context.UserName, context.Password); 

            if (user == null)
            {
                context.SetError("grant_error", "Username or password is wrong.");
                return;
            }

            var identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));
            // role check.
            identity.AddClaim(new Claim(ClaimTypes.Role, "Mod"));

            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                { "audience", (context.ClientId == null) ? string.Empty : context.ClientId }
            });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
            return;
        }
    }
}