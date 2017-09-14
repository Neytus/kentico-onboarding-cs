using Microsoft.Practices.Unity;
using TodoList.Contracts.Dependency;
using TodoList.Contracts.Repository;

namespace TodoList.Repository.Dependency
{
    public class RegisterTypes : IBootstrapper
    {
        public void RegisterType(IUnityContainer container)
        {
            container.RegisterType<INodesRepository, NodesRepository>(new HierarchicalLifetimeManager());
        }
    }
}