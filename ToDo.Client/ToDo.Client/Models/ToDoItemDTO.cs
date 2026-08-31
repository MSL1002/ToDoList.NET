namespace ToDo.Client.Models
{
    public class ToDoItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public bool IsDone { get; set; }
    }
}
