namespace TodoList.Api.Models
{
    public class NodeModel
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public NodeModel(int id, string text)
        {
            Id = id;
            Text = text;
        }
    }
}