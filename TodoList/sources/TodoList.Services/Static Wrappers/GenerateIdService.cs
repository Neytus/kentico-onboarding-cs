using System;
using TodoList.Contracts.Services;

namespace TodoList.Services.Static_Wrappers
{
    internal class GenerateIdService : IGenerateIdService
    {
        public Guid GenerateId()
            => Guid.NewGuid();
    }
}
