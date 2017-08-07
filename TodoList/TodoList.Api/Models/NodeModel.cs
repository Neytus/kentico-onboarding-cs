using System.Runtime.Serialization;

namespace TodoList.Api.Models
{
    [DataContract]
    public class NodeModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Text { get; set; }

        public NodeModel(int id, string text)
        {
            Id = id;
            Text = text;
        }
    }
}