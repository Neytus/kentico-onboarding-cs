using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoList.Contracts.Api;
using TodoList.Contracts.DAL;
using TodoList.Contracts.Models;
using TodoList.Contracts.Services;

namespace TodoList.Api.Controllers
{
    [Route("api/v1/nodes/{id:guid?}", Name = "nodes")]
    public class NodesController : ApiController
    {
        private readonly INodesRepository _repository;
        private readonly ICreateNodeService _createNodeService;
        private readonly ILocationHelper _locationHelper;

        public NodesController(INodesRepository repository, ICreateNodeService createNodeService,
            ILocationHelper locationHelper)
        {
            _repository = repository;
            _createNodeService = createNodeService;
            _locationHelper = locationHelper;
        }

        public async Task<IHttpActionResult> GetAsync()
        {
            return Ok(await _repository.GetAllAsync());
        }

        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            if (!ValidateId(id)) return BadRequest();

            NodeModel returnTask;

            try
            {
                returnTask = await _repository.GetByIdAsync(id);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            if (returnTask == null) return NotFound();

            return Ok(returnTask);
        }

        public async Task<IHttpActionResult> PostAsync([FromBody] NodeModel node)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!ValidateNodeModel(node)) return BadRequest(ModelState);

            try
            {
                var newNode = await _createNodeService.CreateNodeAsync(node);
                return Created(_locationHelper.GetLocation(newNode.Id), newNode);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        public async Task<IHttpActionResult> PutAsync(NodeModel node)
            => Content(HttpStatusCode.Accepted, await _repository.UpdateAsync(
                new NodeModel
                {
                    Id = new Guid("6171ec89-e3b5-458e-ae43-bc0e8ec061e2"),
                    Text = "Planet Music"
                }));

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            return Ok();
        }

        private bool ValidateId(Guid id)
        {
            Guid returnGuid;
            return Guid.TryParse(id.ToString("D"), out returnGuid);
        }

        private bool ValidateNodeModel(NodeModel node)
        {
            return node?.Text != null;
        }
    }
}