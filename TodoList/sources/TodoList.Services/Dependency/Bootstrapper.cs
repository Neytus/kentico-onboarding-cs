using Microsoft.Practices.Unity;
using TodoList.Contracts.Dependency;
using TodoList.Contracts.Services;
using TodoList.Services.Static_Wrappers;
using TodoList.Services.Nodes;

namespace TodoList.Services.Dependency
{
    public class Bootstrapper : IBootstrapper
    {
        public IUnityContainer RegisterType(IUnityContainer container)
        {
            container.RegisterType<IGenerateIdService, GenerateIdService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICreateNodeService, CreateNodeService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICurrentTimeService, CurrentTimeService>(new HierarchicalLifetimeManager());
            container.RegisterType<IUpdateNodeService, UpdateNodeService>(new HierarchicalLifetimeManager());

            return container;
        }
    }
}