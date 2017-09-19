using System.Web.Http;
using Microsoft.Practices.Unity;
using TodoList.Contracts.Dependency;
using Unity.WebApi;

namespace TodoList.Api
{
    internal static class DependencyInjectionConfig
    {
        internal static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer()
                .Register<Dependency.RegisterTypes>()
                .Register<Repository.Dependency.RegisterTypes>();

            config.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static IUnityContainer Register<TRegisterTypes>(this IUnityContainer container)
            where TRegisterTypes : IBootstrapper, new() 
            => new TRegisterTypes().RegisterType(container);
    }
}