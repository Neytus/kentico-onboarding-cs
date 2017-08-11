using Microsoft.Practices.Unity;
using System.Web.Http;
using TodoList.BL;
using TodoList.DAL;
using Unity.WebApi;

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
            container.RegisterType<INodeRepository, NodeRepository>(new HierarchicalLifetimeManager());
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}