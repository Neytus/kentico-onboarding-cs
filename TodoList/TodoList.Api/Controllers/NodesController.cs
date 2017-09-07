using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoList.Contracts.Api;
using TodoList.Contracts.DAL;

namespace TodoList.Api.Controllers
{
    [Route("api/v1/nodes/{id:guid?}", Name = "nodes")]
    public class NodesController : ApiController
    {
        private readonly INodesRepository _repository;
        private readonly ILocationHelper _locationHelper;

        public NodesController(INodesRepository repository, ILocationHelper locationHelper)
        {
            _repository = repository;
            _locationHelper = locationHelper;
        }

        public async Task<IHttpActionResult> GetAsync()
            => Ok(await _repository.GetAllAsync());

        public async Task<IHttpActionResult> GetAsync(Guid id)
            => Ok(await _repository.GetByIdAsync(id));

        public async Task<IHttpActionResult> PostAsync([FromBody] NodeModel model)
        {
            await _repository.AddAsync(model);
            return Created(_locationHelper.GetLocation(model.Id), model);
        }

        public async Task<IHttpActionResult> PutAsync(NodeModel model)
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
    }
}