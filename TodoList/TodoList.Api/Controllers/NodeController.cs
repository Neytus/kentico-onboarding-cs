using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TodoList.Api.Models;

namespace TodoList.Api.Controllers
{
    public class NodeController : ApiController
    {
        public IEnumerable<Node> Get()
        {
            return new List<Node>();
        }

        public Node Get(string id)
        {
            return new Node();
        }

        public void Post(string text)
        {
           
        }

        public void Put(string id, string text)
        {
            
        }
    }
}
