﻿using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using TodoList.Contracts.Models;

namespace TodoList.Api.Tests.Extensions
{
    internal static class NodeModelEqualityComparerWrapper
    {
        private static Lazy<NodeModelEqualityComparer> Lazy => new Lazy<NodeModelEqualityComparer>();

        private static NodeModelEqualityComparer Comparer => Lazy.Value;

        private sealed class NodeModelEqualityComparer : IEqualityComparer<NodeModel>
        {
            public bool Equals(NodeModel x, NodeModel y)
            {
                if ((x == null) || (y == null) || x.GetType() != y.GetType()) return false;

                return (x.Id == y.Id) && (x.Text == y.Text);
            }

            public int GetHashCode(NodeModel obj) => obj.GetHashCode();
        }

        internal static EqualConstraint UsingNodeModelEqualityComparer(this EqualConstraint constraint)
            => constraint.Using(Comparer);
    }
}