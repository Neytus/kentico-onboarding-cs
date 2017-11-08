using Microsoft.Practices.Unity;

namespace TodoList.Contracts.Dependency
{
    public interface IBootstrapper
    {
        IUnityContainer RegisterType(IUnityContainer container);
    }
}