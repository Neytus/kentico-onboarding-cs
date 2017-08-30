using Microsoft.Practices.Unity;
using TodoList.Contracts.DAL;
using TodoList.DAL;

namespace TodoList.Contracts.Dependency
{
    public static class RegisterTypes
    {
        public static UnityContainer Register()
        {
            var container = new UnityContainer();
      
            container.RegisterType<INodesRepository, NodesRepository>(new ContainerControlledLifetimeManager());

            return container;
        }
    }
}
