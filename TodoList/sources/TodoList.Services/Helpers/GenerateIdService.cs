using System;
using TodoList.Contracts.Services;

namespace TodoList.Services.Helpers
{
    internal class GenerateIdService : IGenerateIdService
    {
        public Guid GenerateId()
            => Guid.NewGuid();
    }
}
