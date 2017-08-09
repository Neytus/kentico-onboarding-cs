using System;
using System.Collections.Generic;
using TodoList.Api.Models;

namespace TodoList.Api.Tests.Util
{
    internal static class NodeModelEqualityComparerWrapper
    {
        private static readonly Lazy<NodeModelEqualityComparer> Lazy = new Lazy<NodeModelEqualityComparer>();

        internal static NodeModelEqualityComparer Comparer => Lazy.Value;

        internal sealed class NodeModelEqualityComparer : IEqualityComparer<NodeModel>
        {     
            public bool Equals(NodeModel x, NodeModel y)
            {
                if ((x == null) || (y == null) || x.GetType() != y.GetType()) return false;

                return (x.Id == y.Id) && (x.Text == y.Text);
            }

            public int GetHashCode(NodeModel obj) => obj.GetHashCode();
        }

        internal static bool NodeModelEquals(this NodeModel x, NodeModel y)
        {
            return Comparer.Equals(x, y);
        }
    }
}