﻿using Microsoft.Practices.Unity;
using TodoList.Contracts.Dependency;
using TodoList.Contracts.Repository;
using TodoList.Repository.Helpers;

namespace TodoList.Repository.Dependency
{
    public class RegisterTypes : IBootstrapper
    {
        public IUnityContainer RegisterType(IUnityContainer container) 
            => container.RegisterType<INodesRepository, NodesRepository>(new InjectionConstructor(ConnectionHelper.GetDbConnection()));
    }
}