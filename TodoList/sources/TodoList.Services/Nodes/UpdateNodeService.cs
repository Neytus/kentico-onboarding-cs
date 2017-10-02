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

        public async Task<NodeModel> UpdateNodeAsync(NodeModel node)
        {
            var currentTime = _timeService.GetCurrentTime();

            var chosenNode = await _repository.GetByIdAsync(node.Id);
            chosenNode.Text = node.Text;
            chosenNode.LastUpdate = currentTime;

            await _repository.UpdateAsync(chosenNode);

            return chosenNode;
        }
    }
}