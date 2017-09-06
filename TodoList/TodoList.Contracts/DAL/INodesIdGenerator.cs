using System;

namespace TodoList.Contracts.DAL
{
    public interface INodesIdGenerator
    {
        Guid GenerateId();
    }
}
