using System;
using System.Threading.Tasks;
using TodoList.Contracts.DAL;
using TodoList.Contracts.Models;
using TodoList.Contracts.Services;

namespace TodoList.Services
{
    internal class UpdateNodeService : IUpdateNodeService
    {
        private readonly INodesRepository _repository;
        private readonly ICurrentTimeService _timeService;

        public UpdateNodeService(INodesRepository repository, ICurrentTimeService timeService)
        {
            _repository = repository;
            _timeService = timeService;
        }

        public async Task<NodeModel> UpdateNodeAsync(NodeModel node)
        {
            var currentTime = _timeService.GetCurrentTime();

            var chosenNode = await _repository.GetByIdAsync(node.Id);

            if (chosenNode == null)
            {
                throw new InvalidOperationException("Node does not exist in the database.");
            }

            chosenNode.Text = node.Text;
            chosenNode.LastUpdate = currentTime;

            await _repository.UpdateAsync(chosenNode);

            return chosenNode;
        }

        public async Task<bool> IsInDbAsync(Guid id)
        {
            var foundNode = await _repository.GetByIdAsync(id);
            return foundNode != null;
        }
    }
}
