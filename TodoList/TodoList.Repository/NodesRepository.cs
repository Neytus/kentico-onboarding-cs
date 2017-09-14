﻿using System;
using System.Threading.Tasks;
using TodoList.Contracts.Api;
using TodoList.Contracts.DAL;

namespace TodoList.Repository
{
    internal class NodesRepository : INodesRepository
    {
        private static readonly Guid FirstId = new Guid("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72");
        private static readonly Guid SecondId = new Guid("b84bbcc7-d516-4805-b2e3-20a2df3758a2");
        private static readonly Guid ThirdId = new Guid("6171ec89-e3b5-458e-ae43-bc0e8ec061e2");
        private static readonly Guid FourthId = new Guid("b61670fd-33ce-400e-a351-f960230e3aae");

        private static readonly NodeModel FirstModel = new NodeModel {Id = FirstId, Text = "poopy"};
        private static readonly NodeModel SecondModel = new NodeModel {Id = SecondId, Text = "GEARS"};
        private static readonly NodeModel ThirdModel = new NodeModel {Id = ThirdId, Text = "Planet Music"};
        private static readonly NodeModel FourthModel = new NodeModel {Id = FourthId, Text = "Time to get shwifty"};

        public async Task<NodeModel[]> GetAllAsync()
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