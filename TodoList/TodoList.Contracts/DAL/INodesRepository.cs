using System.Threading.Tasks;
using TodoList.Contracts.Api;

namespace TodoList.Contracts.DAL
{
    public interface INodesRepository
    {
        Task<NodeModel[]> GetAllAsync();

        Task<NodeModel> GetByIdAsync(string id);

        Task<NodeModel> AddAsync(string text);

        Task<NodeModel> UpdateAsync(string id, string text);

        Task DeleteAsync(string id);
    }
}
