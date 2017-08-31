using System.Web.Http;
using Microsoft.Practices.Unity;
using Unity.WebApi;
using TodoList.Contracts.Dependency;

namespace TodoList.Api
{
    public static class UnityConfig
    {
        internal static void RegisterComponents()
        {
            var container = new UnityContainer();

            new RegisterTypes().RegisterType(container);
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}