using System;
using System.Threading.Tasks;
using TodoList.Contracts.Api;

namespace TodoList.Contracts.DAL
{
    public interface INodesRepository
    {
        Task<NodeModel[]> GetAllAsync();

        Task<NodeModel> GetByIdAsync(Guid id);

        Task<NodeModel> AddAsync(NodeModel model);

        Task<NodeModel> UpdateAsync(NodeModel model);

        Task DeleteAsync(Guid id);
    }
}
