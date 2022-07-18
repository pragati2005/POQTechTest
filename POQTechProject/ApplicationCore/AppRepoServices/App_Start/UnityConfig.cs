using Repository;
using Repository.Abstraction;
using Repository.Implementation;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace AppRepoServices
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IMockyDBContext, MockyDBContext>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}