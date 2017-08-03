using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace TodoList.Api.Models
{
    public class NodeModel
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public NodeModel() : this(0, "default text")
        {
        }

        public NodeModel(int id, string text)
        {
            Id = id;
            Text = text;
        }
    }
}