using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoList.Contracts.Api;
using TodoList.Contracts.Models;
using TodoList.Contracts.Repository;
using TodoList.Contracts.Services;
using static System.String;

namespace TodoList.Api.Controllers
{
    [Route("api/v1/nodes/{id:guid?}", Name = "nodes")]
    public class NodesController : ApiController
    {
        private readonly INodesRepository _repository;
        private readonly ICreateNodeService _createNodeService;
        private readonly IUpdateNodeService _updateNodeService;
        private readonly ILocator _locator;

        public NodesController(
            INodesRepository repository,
            ICreateNodeService createNodeService,
            IUpdateNodeService updateNodeService,
            ILocator locator)
        {
            _repository = repository;
            _createNodeService = createNodeService;
            _updateNodeService = updateNodeService;
            _locator = locator;
        }

        public async Task<IHttpActionResult> GetAsync()
            => Ok(await _repository.GetAllAsync());

        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            ValidateId(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingNode = await _repository.GetByIdAsync(id);

            if (existingNode == null)
            {
                return NotFound();
            }

            return Ok(existingNode);
        }

        public async Task<IHttpActionResult> PostAsync([FromBody] NodeModel node)
        {
            ValidatePostNodeModel(node);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newNode = await _createNodeService.CreateNodeAsync(node);

            return Created(_locator.GetNodeLocation(newNode.Id), newNode);
        }

        public async Task<IHttpActionResult> PutAsync([FromBody] NodeModel node)
        {
            ValidatePutNodeModel(node);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _updateNodeService.IsInDbAsync(node.Id))
            {
                var updatedNode = await _updateNodeService.UpdateNodeAsync(node);
                return Content(HttpStatusCode.Accepted, updatedNode);
            }

            var newNode = await _createNodeService.CreateNodeAsync(node);
            var location = _locator.GetNodeLocation(newNode.Id);
            return Created(location, newNode);
        }

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            ValidateId(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _updateNodeService.IsInDbAsync(id))
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }

        private void ValidatePutNodeModel(NodeModel node)
        {
            if (node == null)
            {
                ModelState.AddModelError(nameof(node), "Node model is not correctly defined.");
                return;
            }
            if (node.Id == Guid.Empty)
            {
                ModelState.AddModelError(nameof(node.Id), "Node model requires a specified id parameter.");
            }

            ValidateNodeModel(node);
        }

        private void ValidatePostNodeModel(NodeModel node)
        {
            if (node == null)
            {
                ModelState.AddModelError(Empty, "Node model is not correctly defined.");
                return;
            }
            if (node.Id != Guid.Empty)
            {
                ModelState.AddModelError(nameof(node.Id), "Node model can't have id parameter specified here.");
            }

            ValidateNodeModel(node);
        }

        private void ValidateId(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return;
            }
            if (id == default(Guid))
            {
                ModelState.AddModelError(nameof(id), "Id attribute is not valid");
            }
        }

        private void ValidateNodeModel(NodeModel node)
        {
            if (IsNullOrWhiteSpace(node.Text))
            {
                ModelState.AddModelError(nameof(node.Text), "Text can't be null or whitespace.");
            }

            if (node.Creation != default(DateTime))
            {
                ModelState.AddModelError(nameof(node.Creation),
                    "Node model can't have creation time specified here.");
            }
            if (node.LastUpdate != default(DateTime))
            {
                ModelState.AddModelError(nameof(node.LastUpdate),
                    "Node model can't have last update time specified here.");
            }
        }
    }
}