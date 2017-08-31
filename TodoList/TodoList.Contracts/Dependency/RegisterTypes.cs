using Microsoft.Practices.Unity;
using TodoList.Contracts.DAL;
using TodoList.DAL;

namespace TodoList.Contracts.Dependency
{
    public class RegisterTypes : IRegisterTypes
    {
        public void RegisterType(IUnityContainer container)
        {
            container.RegisterType<INodesRepository, NodesRepository>(new ContainerControlledLifetimeManager());
        }
    }
}