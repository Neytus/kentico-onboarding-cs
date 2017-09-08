using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using TodoList.Contracts.DAL;
using TodoList.Contracts.Dependency;
using TodoList.Services;

namespace TodoList.Api.Services.Dependency
{
    public class RegisterTypes : IRegisterTypes
    {
        public void RegisterType(IUnityContainer container)
        {
            container.RegisterType<IGenerateIdService, GenerateIdService>(new ContainerControlledLifetimeManager());
        }
    }
}