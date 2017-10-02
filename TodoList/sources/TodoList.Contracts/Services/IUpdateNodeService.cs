using System.Threading.Tasks;
using TodoList.Contracts.Models;

namespace TodoList.Contracts.Services
{
    public interface IUpdateNodeService
    {
        Task<NodeModel> UpdateNodeAsync(NodeModel nodeValues);
    }
}