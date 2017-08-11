using System.Threading.Tasks;

namespace TodoList.BL
{
    public interface INodeRepository
    {
        Task<NodeDto[]> GetAllAsync();

        Task<NodeDto> GetByIdAsync(string id);

        Task<NodeDto> AddAsync(string text);

        Task<NodeDto> UpdateAsync(string id, string text);

        Task<NodeDto> DeleteAsync(string id);
    }
}
