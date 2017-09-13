using Microsoft.Practices.Unity;
using TodoList.Contracts.DAL;
using TodoList.Contracts.Dependency;

namespace TodoList.DAL.Dependency
{
    public class RegisterTypes : IBootstrapper
    {
        public void RegisterType(IUnityContainer container)
        {
            container.RegisterType<INodesRepository, NodesRepository>(new HierarchicalLifetimeManager());
        }
    }
}