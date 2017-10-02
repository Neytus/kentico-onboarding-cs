using System;
using System.Threading.Tasks;
using TodoList.Contracts.Models;
using TodoList.Contracts.Repository;
using TodoList.Contracts.Services;

namespace TodoList.Services.Nodes
{
    internal class UpdateNodeService : IUpdateNodeService
    {
        private readonly INodesRepository _repository;
        private readonly ICurrentTimeService _timeService;

        private NodeModel _cachedNode;

        public UpdateNodeService(INodesRepository repository, ICurrentTimeService timeService)
        {
            _repository = repository;
            _timeService = timeService;
        }

        public async Task<NodeModel> UpdateNodeAsync(NodeModel nodeValues)
        {
            var currentTime = _timeService.GetCurrentTime();

            if (_cachedNode != null) {
                _cachedNode.Text = nodeValues.Text;
                _cachedNode.LastUpdate = currentTime;
                await _repository.UpdateAsync(_cachedNode);

                return _cachedNode;
            }

            var existingNode = await _repository.GetByIdAsync(nodeValues.Id);
            existingNode.Text = nodeValues.Text;
            existingNode.LastUpdate = currentTime;

            await _repository.UpdateAsync(existingNode);

            return existingNode;
        }

        public async Task<bool> IsInDbAsync(Guid id)
        {
            _cachedNode = await _repository.GetByIdAsync(id);

            return _cachedNode != null;
        }
    }
}