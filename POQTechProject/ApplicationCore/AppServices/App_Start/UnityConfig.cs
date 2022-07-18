using Repository;
using Repository.Abstraction;
using Repository.Implementation;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace AppServices
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IMockyDBContext, MockyDBContext>();
            container.RegisterType<IProductRepository, ProductRepository>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}