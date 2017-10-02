using Microsoft.Practices.Unity;
using TodoList.Contracts.Dependency;
using TodoList.Contracts.Repository;
using TodoList.Repository.Static_Wrappers;

namespace TodoList.Repository.Dependency
{
    public class Bootstrapper : IBootstrapper
    {
        public IUnityContainer RegisterType(IUnityContainer container) 
            => container.RegisterType<INodesRepository, NodesRepository>(new InjectionConstructor(DatabaseConnector.GetDbConnection()));
    }
}