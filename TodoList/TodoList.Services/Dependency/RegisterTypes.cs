﻿using Microsoft.Practices.Unity;
using TodoList.Contracts.Dependency;
using TodoList.Contracts.Services;
using TodoList.Services.Helpers;

namespace TodoList.Services.Dependency
{
    public class RegisterTypes : IRegisterTypes
    {
        public void RegisterType(IUnityContainer container)
        {
            container.RegisterType<IGenerateIdService, GenerateIdService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICreateNodeService, CreateNodeService>(new ContainerControlledLifetimeManager());
        }
    }
}