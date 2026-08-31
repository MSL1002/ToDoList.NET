using System.ComponentModel.DataAnnotations;

namespace ToDo.Client.Models
{
    public class ToDoItemCreateDTO
    {
        [Required(ErrorMessage = "A title is required")]
        public string Title { get; set; } = string.Empty;

        [DataType(DataType.MultilineText)]
        public string Details { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; } = DateTime.Today;

        public bool IsDone { get; set; }
        
    }
}
