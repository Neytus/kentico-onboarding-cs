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

        public NodesController() { }

        public NodesController(INodesRepository repository, ILocationHelper locationHelper)
        {
            _repository = repository;
            _locationHelper = locationHelper;
        }

        public async Task<IHttpActionResult> GetAsync()
            => await Task.FromResult<IHttpActionResult>(Ok(_repository.GetAllAsync().Result));

        public async Task<IHttpActionResult> GetAsync(Guid id)
            => await Task.FromResult<IHttpActionResult>(Ok(_repository
                .GetByIdAsync(new Guid("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72")).Result));

        public async Task<IHttpActionResult> PostAsync(NodeModel model)
        {
            var returnedModel = _repository.AddAsync(new NodeModel { Text = "text" }).Result;
            return await Task.FromResult<IHttpActionResult>(
                Created(_locationHelper.GetLocation(Request, returnedModel.Id), returnedModel));
        }

        public async Task<IHttpActionResult> PutAsync(NodeModel model)
            => await Task.FromResult<IHttpActionResult>(Content(HttpStatusCode.Accepted,
                _repository.UpdateAsync(new NodeModel
                {
                    Id = new Guid("6171ec89-e3b5-458e-ae43-bc0e8ec061e2"),
                    Text = "Planet Music"
                }).Result));

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
            => await Task.FromResult<IHttpActionResult>(Ok());
    }
}