using System;
using System.Collections.Generic;
using TodoList.Api.Models;

namespace TodoList.Api.Tests.Util
{
    public sealed class NodeModelEqualityComparer : IEqualityComparer<NodeModel>
    {
        private static readonly Lazy<NodeModelEqualityComparer> Lazy = new Lazy<NodeModelEqualityComparer>();

        public static NodeModelEqualityComparer Instance => Lazy.Value;

        private NodeModelEqualityComparer()
        {
        }

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
