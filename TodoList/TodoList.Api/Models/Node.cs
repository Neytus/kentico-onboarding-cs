using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoList.Api.Models
{
    public class Node
    {
        public Node() : this("default id", "default text")
        {      
        }

        public Node(string id, string text)
        {
            Id = id;
            Text = text;
        }

        public string Id { get; set; }
        public string Text { get; set; }
    }
}