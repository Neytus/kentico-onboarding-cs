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

        public UpdateNodeService(INodesRepository repository, ICurrentTimeService timeService)
        {
            _repository = repository;
            _timeService = timeService;
        }

        public async Task<NodeModel> UpdateNodeAsync(NodeModel nodeValues)
        {
            var currentTime = _timeService.GetCurrentTime();

            var existingNode = await _repository.GetByIdAsync(nodeValues.Id);
            existingNode.Text = nodeValues.Text;
            existingNode.LastUpdate = currentTime;

            await _repository.UpdateAsync(existingNode);

            return existingNode;
        }
    }
}