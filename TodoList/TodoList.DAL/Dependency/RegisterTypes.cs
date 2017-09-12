using Microsoft.Practices.Unity;
using TodoList.Contracts.DAL;
using TodoList.Contracts.Dependency;
using TodoList.DAL.Helpers;

namespace TodoList.DAL.Dependency
{
    public class RegisterTypes : IRegisterTypes
    {
        public void RegisterType(IUnityContainer container)
        {
            container.RegisterType<INodesRepository, NodesRepository>(new InjectionConstructor(ConnectionHelper.GetDbConnection()));
        }
    }
}