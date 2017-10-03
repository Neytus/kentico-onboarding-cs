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
            if (nodeValues == null)
            {
                throw new InvalidOperationException("Values to update have to be provided.");
            }

            var currentTime = _timeService.GetCurrentTime();
            CheckCachedNode(nodeValues.Id);

            if (_cachedNode == null) {
                _cachedNode = await _repository.GetByIdAsync(nodeValues.Id);
            }

            _cachedNode.Text = nodeValues.Text;
            _cachedNode.LastUpdate = currentTime;

            await _repository.UpdateAsync(_cachedNode);

            return _cachedNode;
        }

        private void CheckCachedNode(Guid id)
        {
            if (_cachedNode == null)
            {
                return;
            }
            if (_cachedNode.Id != id)
            {
                _cachedNode = null;
            }
        }

        public async Task<bool> IsInDbAsync(Guid id)
        {
            _cachedNode = await _repository.GetByIdAsync(id);

            return _cachedNode != null;
        }
    }
}