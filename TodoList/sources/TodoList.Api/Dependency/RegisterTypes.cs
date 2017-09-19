using System.Net.Http;
using System.Web;
using Microsoft.Practices.Unity;
using TodoList.Api.Helpers;
using TodoList.Contracts.Api;
using TodoList.Contracts.Dependency;

namespace TodoList.Api.Dependency
{
    public class RegisterTypes : IBootstrapper
    {
        public IUnityContainer RegisterType(IUnityContainer container)
        {
            container.RegisterType<HttpRequestMessage>(new HierarchicalLifetimeManager(), new InjectionFactory(GetRequestMessage));
            container.RegisterType<ILocationHelper, LocationHelper>(new HierarchicalLifetimeManager());

            return container;
        }

        private static HttpRequestMessage GetRequestMessage(IUnityContainer container)
            => (HttpRequestMessage) HttpContext.Current.Items["MS_HttpRequestMessage"];
    }
}