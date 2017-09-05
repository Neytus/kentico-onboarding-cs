using System.Net.Http;
using Microsoft.Practices.Unity;
using TodoList.Api.Helpers;
using TodoList.Contracts.Dependency;

namespace TodoList.Api.Dependency
{
    public class RegisterTypes : IRegisterTypes
    {
        public void RegisterType(IUnityContainer container)
        {
            container.RegisterType<ILocationHelper, LocationHelper>(new InjectionConstructor(typeof(HttpRequestMessage)));
        }
    }
}