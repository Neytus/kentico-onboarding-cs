using System;
using System.Threading.Tasks;
using TodoList.Contracts.DAL;
using TodoList.Contracts.Models;
using TodoList.Contracts.Services;

namespace TodoList.Api.Services
{
    internal class CreateNodeService: ICreateNodeService
    {
        private readonly INodesRepository _repository;
        private readonly IGenerateIdService _generateIdService;

        public CreateNodeService(INodesRepository repository, IGenerateIdService generateIdService)
        {
            _repository = repository;
            _generateIdService = generateIdService;
        }

        public async Task<NodeModel> CreateNodeAsync(string text) 
            => await CreateNodeAsync(text, _generateIdService.GenerateId());

        public async Task<NodeModel> CreateNodeAsync(string text, Guid id)
        {
            var newModel = new NodeModel{Id = id, Text = text};

            await _repository.AddAsync(newModel);

            return newModel;
        }
    }
}
