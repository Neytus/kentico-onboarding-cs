using Microsoft.Practices.Unity;
using TodoList.Contracts.Dependency;
using TodoList.Contracts.Repository;

namespace TodoList.Repository.Dependency
{
    public class Bootstrapper : IBootstrapper
    {
        public IUnityContainer RegisterType(IUnityContainer container) 
            => container.RegisterType<INodesRepository, NodesRepository>(new ContainerControlledLifetimeManager());
    }
}