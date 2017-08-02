using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoList.Api.Models
{
    public class Node
    {
        public Node() : this(0, "default text")
        {      
        }

        public Node(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public int Id { get; set; }
        public string Text { get; set; }
    }
}