using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Api.Models;

namespace TodoList.Api.Tests.Util
{
    public class NodeModelEqualityComparer : IEqualityComparer<NodeModel>
    {
        public bool Equals(NodeModel x, NodeModel y)
        {
            if ((x == null) || (y == null) || x.GetType() != y.GetType()) return false;

            return (x.Id == y.Id) && (x.Text == y.Text);
        }

        public int GetHashCode(NodeModel obj)
        {
            return obj.GetHashCode();
        }
    }
}
