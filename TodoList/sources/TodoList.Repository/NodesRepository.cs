using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Contracts.Models;
using TodoList.Contracts.Repository;

namespace TodoList.Repository
{
    internal class NodesRepository : INodesRepository
    {
        private static readonly NodeModel FirstModel = new NodeModel {Id = new Guid("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72"), Text = "poopy"};
        private static readonly NodeModel SecondModel = new NodeModel {Id = new Guid("b84bbcc7-d516-4805-b2e3-20a2df3758a2"), Text = "GEARS"};
        private static readonly NodeModel ThirdModel = new NodeModel {Id = new Guid("6171ec89-e3b5-458e-ae43-bc0e8ec061e2"), Text = "Planet Music"};
        private static readonly NodeModel FourthModel = new NodeModel {Id = new Guid("b61670fd-33ce-400e-a351-f960230e3aae"), Text = "Time to get shwifty"};

        public async Task<IEnumerable<NodeModel>> GetAllAsync()
            => await Task.FromResult(new[]
            {
                FirstModel,
                SecondModel,
                ThirdModel,
                FourthModel
            });

        public async Task<NodeModel> GetByIdAsync(Guid id)
            => await Task.FromResult(FirstModel);

        public async Task<NodeModel> AddAsync(NodeModel model)
            => await Task.FromResult(SecondModel);

        public async Task<NodeModel> UpdateAsync(NodeModel model)
            => await Task.FromResult(ThirdModel);

        public async Task DeleteAsync(Guid id)
            => await Task.CompletedTask;
    }
}