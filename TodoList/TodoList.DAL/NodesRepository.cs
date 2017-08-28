using System;
using System.Threading.Tasks;
using TodoList.Contracts.Api;
using TodoList.Contracts.DAL;

namespace TodoList.DAL
{
    public class NodesRepository : INodesRepository
    {
        private static readonly Guid FirstId = new Guid("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72");
        private static readonly Guid SecondId = new Guid("b84bbcc7-d516-4805-b2e3-20a2df3758a2");
        private static readonly Guid ThirdId = new Guid("6171ec89-e3b5-458e-ae43-bc0e8ec061e2");
        private static readonly Guid FourthId = new Guid("b61670fd-33ce-400e-a351-f960230e3aae");

        public async Task<NodeModel[]> GetAllAsync() => await Task.FromResult(new[]
        {
            new NodeModel {Id = FirstId, Text = "poopy"},
            new NodeModel {Id = SecondId, Text = "GEARS"},
            new NodeModel {Id = ThirdId, Text = "Planet Music"},
            new NodeModel {Id = FourthId, Text = "Time to get shwifty"}
        });

        public async Task<NodeModel> GetByIdAsync(string id) => await Task.FromResult(new NodeModel
        {
            Id = FirstId,
            Text = "poopy"
        });

        public async Task<NodeModel> AddAsync(string text) => await Task.FromResult(new NodeModel
        {
            Id = SecondId,
            Text = "GEARS"
        });

        public async Task<NodeModel> UpdateAsync(string id, string text) => await Task.FromResult(new NodeModel
        {
            Id = ThirdId,
            Text = "Planet Music"
        });

        public async Task DeleteAsync(string id) => await Task.CompletedTask;
    }
}