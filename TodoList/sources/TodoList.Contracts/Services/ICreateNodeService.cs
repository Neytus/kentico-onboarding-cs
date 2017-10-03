using System;
using System.Threading.Tasks;
using TodoList.Contracts.Models;

namespace TodoList.Contracts.Services
{
    public interface ICreateNodeService
    {
        Task<NodeModel> CreateNodeAsync(NodeModel node);
    }
}