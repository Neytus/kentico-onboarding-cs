using Microsoft.Practices.Unity;

namespace TodoList.Contracts.Dependency
{
    public interface IRegisterTypes
    {
        void RegisterType(IUnityContainer container);
    }
}
