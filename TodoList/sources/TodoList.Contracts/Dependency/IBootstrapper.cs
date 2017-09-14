using Microsoft.Practices.Unity;

namespace TodoList.Contracts.Dependency
{
    public interface IBootstrapper
    {
        void RegisterType(IUnityContainer container);
    }
}
