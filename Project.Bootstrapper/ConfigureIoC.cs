using AuthServer.Api.Auth;
using Autofac;
using Autofac.Integration.WebApi;
using Project.Core;
using Project.Core.Identity;
using Project.Data;
using Project.Infrastructure;
using System.IO;
using System.Reflection;
using System.Web.Http;

namespace Project.Bootstrapper
{
    public class ConfigureIoC
    {
        public static IContainer Register(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            string searchPattern = "*.Api.dll";
            var directory = new DirectoryInfo(System.IO.Path.Combine("~/", System.AppDomain.CurrentDomain.BaseDirectory, System.AppDomain.CurrentDomain.RelativeSearchPath ?? ""));
            foreach (var assembly in directory.GetFiles(searchPattern))
            {
                builder.RegisterApiControllers(Assembly.LoadFrom(assembly.FullName));
            }

            builder.RegisterType<AppDbContext>().As<IDbContext>().InstancePerLifetimeScope();
            builder.RegisterType<AuthRepository>().As<IAuthRepository>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerRequest();
            builder.RegisterType<AppUserStore>().As<IAppUserStore>().InstancePerRequest();
            builder.RegisterType<AppUserManager>().AsSelf().InstancePerRequest();

            var container = builder.Build();
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            return container;
        }
    }
}
