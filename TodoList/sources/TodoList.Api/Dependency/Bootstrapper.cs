using System.Net.Http;
using System.Web;
using Microsoft.Practices.Unity;
using TodoList.Api.Static_Wrappers;
using TodoList.Contracts.Api;
using TodoList.Contracts.Dependency;

namespace TodoList.Api.Dependency
{
    public class Bootstrapper : IBootstrapper
    {
        public IUnityContainer RegisterType(IUnityContainer container)
        {
            container.RegisterType<HttpRequestMessage>(new HierarchicalLifetimeManager(), new InjectionFactory(GetRequestMessage));
            container.RegisterType<ILocator, Locator>(new HierarchicalLifetimeManager());
            container.RegisterType<IDatabaseConnector, DatabaseConnector>(new HierarchicalLifetimeManager());

            return container;
        }

        private static HttpRequestMessage GetRequestMessage(IUnityContainer container)
            => (HttpRequestMessage) HttpContext.Current.Items["MS_HttpRequestMessage"];
    }
}