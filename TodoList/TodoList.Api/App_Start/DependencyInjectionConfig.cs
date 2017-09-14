using System.Web.Http;
using Microsoft.Practices.Unity;
using TodoList.Contracts.Dependency;
using Unity.WebApi;

namespace TodoList.Api
{
    internal static class DependencyInjectionConfig
    {
        internal static void RegisterComponents()
        {
            var container = new UnityContainer();
            Register<Dependency.RegisterTypes>(container);
            Register<Repository.Dependency.RegisterTypes>(container);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static void Register<TRegisterTypes>(this IUnityContainer container)
            where TRegisterTypes : IBootstrapper, new()
        {
            var register = new TRegisterTypes();
            register.RegisterType(container);
        }
    }
}