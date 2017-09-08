using System;

namespace TodoList.Contracts.Services
{
    public interface IGenerateIdService
    {
        Guid GenerateId();
    }
}
