using Microsoft.Practices.Unity;
using TodoList.Contracts.Dependency;
using TodoList.Contracts.Services;
using TodoList.Services.Static_Wrappers;
using TodoList.Services.Nodes;
using TodoList.Services.Static_Wrappers;

namespace TodoList.Services.Dependency
{
    public class Bootstrapper : IBootstrapper
    {
        public IUnityContainer RegisterType(IUnityContainer container)
        {
            container.RegisterType<IGenerateIdService, GenerateIdService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICreateNodeService, CreateNodeService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICurrentTimeService, CurrentTimeService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUpdateNodeService, UpdateNodeService>(new ContainerControlledLifetimeManager());

            return container;
        }
    }
}