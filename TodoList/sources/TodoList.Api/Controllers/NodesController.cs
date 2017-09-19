using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoList.Contracts.Api;
using TodoList.Contracts.Models;
using TodoList.Contracts.Repository;
using TodoList.Contracts.Services;

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
            if (!ValidateId(id)) return BadRequest("Invalid id format.");

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
            if (!ValidatePostNodeModel(node)) return BadRequest(ModelState);

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
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!ValidatePutNodeModel(node)) return BadRequest(ModelState);

            NodeModel updatedNode;

            try
            {
                updatedNode = await _updateNodeService.UpdateNodeAsync(node);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Content(HttpStatusCode.Accepted, updatedNode);
        }

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            if (!ValidateId(id)) return BadRequest(ModelState);

            var isInDb = await _updateNodeService.IsInDbAsync(id);

            if (!isInDb) return BadRequest("Node does not exist in the database");
            await _repository.DeleteAsync(id);
            return Ok();
        }

        private bool ValidateId(Guid id)
        {
            Guid returnGuid;
            if (Guid.TryParse(id.ToString("D"), out returnGuid)) return true;
            ModelState.AddModelError(id.ToString(), "Invalid id format.");
            return false;
        }

        private bool ValidatePostNodeModel(NodeModel node)
        {
            if (node.Id == Guid.Empty) return node.Text != null;
            ModelState.AddModelError(node.Text, "Id value can't be specified here.");
            return false;
        }

        private bool ValidatePutNodeModel(NodeModel node)
        {
            if (node.Id != Guid.Empty) return node.Text != null;
            ModelState.AddModelError(node.Text, "Id value has to be specified.");
            return false;
        }
    }
}