using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using TodoList.Contracts.DAL;
using TodoList.DAL;

namespace TodoList.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();        
            container.RegisterType<INodesRepository, NodesRepository>(new ContainerControlledLifetimeManager());
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}