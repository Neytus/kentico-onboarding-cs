using System.Web.Http;
using Microsoft.Practices.Unity;
using Unity.WebApi;

namespace TodoList.Api
{
    internal static class DependencyInjectionConfig
    {
        internal static void RegisterComponents()
        {
            var container = new UnityContainer();
            new Repository.Dependency.RegisterTypes().RegisterType(container);
            new Dependency.RegisterTypes().RegisterType(container);
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}