using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace TodoList.Contracts.Dependency
{
    public interface IRegisterTypes
    {
        void RegisterType(IUnityContainer container);
    }
}
