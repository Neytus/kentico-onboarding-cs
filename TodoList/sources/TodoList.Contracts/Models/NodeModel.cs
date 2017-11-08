using System;

namespace TodoList.Contracts.Models
{
    public class NodeModel
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTime Creation { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}