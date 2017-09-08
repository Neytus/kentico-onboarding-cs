using System;
using TodoList.Contracts.Services;

namespace TodoList.Services
{
    internal class GenerateIdService : IGenerateIdService
    {
        public Guid GenerateId() 
            => Guid.NewGuid();
    }
}
