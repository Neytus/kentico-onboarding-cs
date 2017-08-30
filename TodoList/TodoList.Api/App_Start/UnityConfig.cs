using System.Web.Http;
using Unity.WebApi;
using TodoList.Contracts.Dependency;

namespace TodoList.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = RegisterTypes.Register();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}