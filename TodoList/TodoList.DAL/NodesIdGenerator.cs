using System;
using TodoList.Contracts.DAL;

namespace TodoList.DAL
{
    internal class NodesIdGenerator : INodesIdGenerator
    {
        public Guid GenerateId()
        {
            return new Guid();
        }
    }
}
