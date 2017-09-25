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
        private readonly ILocationHelper _locationHelper;

        public NodesController(
            INodesRepository repository,
            ICreateNodeService createNodeService,
            IUpdateNodeService updateNodeService,
            ILocationHelper locationHelper)
        {
            _repository = repository;
            _createNodeService = createNodeService;
            _updateNodeService = updateNodeService;
            _locationHelper = locationHelper;
        }

        public async Task<IHttpActionResult> GetAsync()
            => Ok(await _repository.GetAllAsync());

        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var returnedNode = await _repository.GetByIdAsync(id);

            if (returnedNode == null) return NotFound();

            return Ok(returnedNode);
        }

        public async Task<IHttpActionResult> PostAsync([FromBody] NodeModel node)
        {
            if (!ValidatePostNodeModel(node)) return BadRequest(ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            NodeModel newNode;

            try
            {
                newNode = await _createNodeService.CreateNodeAsync(node);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Created(_locationHelper.GetNodeLocation(newNode.Id), newNode);
        }

        public async Task<IHttpActionResult> PutAsync(NodeModel node)
        {
            if (!ValidatePutNodeModel(node)) return BadRequest(ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                if (!await _updateNodeService.IsInDbAsync(node.Id))
                {
                    var newNode = await _createNodeService.CreateNodeAsync(node, node.Id);
                    return Created(_locationHelper.GetNodeLocation(newNode.Id), newNode);
                }

                var updatedNode = await _updateNodeService.UpdateNodeAsync(node);
                return Content(HttpStatusCode.Accepted, updatedNode);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isInDb = await _updateNodeService.IsInDbAsync(id);

            if (!isInDb) return NotFound();

            await _repository.DeleteAsync(id);

            return Ok();
        }

        private bool ValidateNodeText(string text)
        {
            if (!IsNullOrWhiteSpace(text)) return true;

            ModelState.AddModelError(text, "Text can't be null or whitespace.");
            return false;
        }

        private bool ValidatePostNodeModel(NodeModel node)
        {
            if (node.Id == Guid.Empty) return ValidateNodeText(node.Text);

            ModelState.AddModelError(node.Text, "Id value can't be specified here.");

            return false;
        }

        private bool ValidatePutNodeModel(NodeModel node)
        {
            if (node.Id != Guid.Empty) return ValidateNodeText(node.Text);

            ModelState.AddModelError(node.Text, "Id value has to be specified.");

            return false;
        }
    }
}