using System;
using System.Threading.Tasks;
using TodoList.Contracts.Models;
using TodoList.Contracts.Repository;
using TodoList.Contracts.Services;

namespace TodoList.Services.Nodes
{
    internal class CreateNodeService : ICreateNodeService
    {
        private readonly INodesRepository _repository;
        private readonly IGenerateIdService _generateIdService;
        private readonly ICurrentTimeService _timeService;

        public CreateNodeService(INodesRepository repository, IGenerateIdService generateIdService, ICurrentTimeService timeService)
        {
            _repository = repository;
            _generateIdService = generateIdService;
            _timeService = timeService;
        }

        public async Task<NodeModel> CreateNodeAsync(NodeModel node)
        {
            if (node == null)
            {
                throw new InvalidOperationException("Text to add has to be provided.");
            }

            var id = _generateIdService.GenerateId();

            var currentTime = _timeService.GetCurrentTime();

            var newModel = new NodeModel
            {
                Id = id,
                Text = node.Text,
                Creation = currentTime,
                LastUpdate = currentTime
            };

            await _repository.AddAsync(newModel);

            return newModel;
        }
    }
}