using System;
using System.Threading.Tasks;
using TodoList.Contracts.DAL;
using TodoList.Contracts.Models;
using TodoList.Contracts.Services;

namespace TodoList.Services
{
    internal class CreateNodeService : ICreateNodeService
    {
        private readonly INodesRepository _repository;
        private readonly IGenerateIdService _generateIdService;

        public CreateNodeService(INodesRepository repository, IGenerateIdService generateIdService)
        {
            _repository = repository;
            _generateIdService = generateIdService;
        }

        public async Task<NodeModel> CreateNodeAsync(NodeModel node)
            => await CreateNodeAsync(node, _generateIdService.GenerateId());

        public async Task<NodeModel> CreateNodeAsync(NodeModel node, Guid id)
        {
            var newModel = new NodeModel { Id = id, Text = node.Text };

            await _repository.AddAsync(newModel);

            return newModel;
        }
    }
}
