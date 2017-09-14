using Microsoft.Practices.Unity;
using TodoList.Contracts.DAL;
using TodoList.Contracts.Dependency;

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