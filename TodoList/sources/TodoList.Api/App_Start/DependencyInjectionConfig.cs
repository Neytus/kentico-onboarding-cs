using System.Web.Http;
using Microsoft.Practices.Unity;
using TodoList.Api.Dependency;
using TodoList.Contracts.Dependency;
using Unity.WebApi;

namespace TodoList.Api
{
    internal static class DependencyInjectionConfig
    {
        internal static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.Register<RegisterTypes>()
                .Register<Repository.Dependency.RegisterTypes>()
                .Register<Services.Dependency.RegisterTypes>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static IUnityContainer Register<TRegisterTypes>(this IUnityContainer container)
            where TRegisterTypes : IBootstrapper, new()
        {
            var register = new TRegisterTypes();
            return register.RegisterType(container);
        }
    }
}