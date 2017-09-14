using System;

namespace TodoList.Contracts.Api
{
    public class NodeModel
    {
        public Guid Id { get; set; }

        public string Text { get; set; }
    }
}