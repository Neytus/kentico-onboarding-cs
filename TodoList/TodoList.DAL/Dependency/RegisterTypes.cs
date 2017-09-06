using Microsoft.Practices.Unity;
using TodoList.Contracts.DAL;
using TodoList.Contracts.Dependency;

namespace TodoList.DAL.Dependency
{
    public class RegisterTypes : IRegisterTypes
    {
        public void RegisterType(IUnityContainer container)
        {
            container.RegisterType<INodesRepository, NodesRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<INodesFactory, NodesFactory>(new ContainerControlledLifetimeManager());
            container.RegisterType<INodesIdGenerator, NodesIdGenerator>(new ContainerControlledLifetimeManager());
        }
    }
}