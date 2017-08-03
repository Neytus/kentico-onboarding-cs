﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoList.Api.Models
{
    public class NodeModel
    {
        public NodeModel() : this(0, "default text")
        {      
        }

        public NodeModel(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public int Id { get; set; }
        public string Text { get; set; }
    }
}