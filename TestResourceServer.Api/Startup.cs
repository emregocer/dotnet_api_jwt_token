using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Owin;
using Project.Bootstrapper;
using Project.Data;
using System;
using System.Data.Entity;
using System.Web.Http;

[assembly: OwinStartup("ResourceServerConfig", typeof(TestResourceServer.Api.Startup))]
namespace TestResourceServer.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            // seeding the db.
            // requires reference because of this. to ef and project.core .
            try
            {
                Database.SetInitializer(new DatabaseInitializer<AppDbContext>());
                using (var context = new AppDbContext())
                {
                    context.Database.Initialize(force: true);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while initializing db: " + ex);
            }

            // setting up the autofac.
            var container = ConfigureIoC.Register(config);

            config.MapHttpAttributeRoutes();
            ConfigureOAuth(app);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var issuer = "http://localhost:50967";
            var audience = "91493eb12406451599cd6f2f868f71dc";
            var secret = TextEncodings.Base64Url.Decode("FtbS5vFaccdXuLWKAxJjhaYqZKoLF543xG4vA6JEMOw");

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { audience },
                IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[]
                    {
                        new SymmetricKeyIssuerSecurityKeyProvider(issuer, secret)
                    }
            });
        }
    }
}