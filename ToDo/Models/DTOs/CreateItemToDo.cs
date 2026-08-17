using System.ComponentModel.DataAnnotations;

namespace ToDo.Models.DTOs
{
    public class CreateItemToDo
    {

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = String.Empty;

        [DataType(DataType.MultilineText)]
        public string Details { get; set; } = String.Empty;

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Display(Name = "Is Done")]
        public bool IsDone { get; set; }
    }
}
