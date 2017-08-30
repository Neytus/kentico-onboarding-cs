using System;
using System.Threading.Tasks;
using TodoList.Contracts.Api;

namespace TodoList.Contracts.DAL
{
    public interface INodesRepository
    {
        Task<NodeModel[]> GetAllAsync();

        Task<NodeModel> GetByIdAsync(Guid id);

        Task<NodeModel> AddAsync(string text);

        Task<NodeModel> UpdateAsync(Guid id, string text);

        Task DeleteAsync(Guid id);
    }
}
