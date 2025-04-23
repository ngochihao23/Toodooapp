using System.ComponentModel.DataAnnotations;
using Todoapp.Const;

namespace Todoapp.DTOS.Tasks
{
    public class TaskUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }
        public Guid? CategoryId { get; set; }
        
        public string Tittle { get; set; }

        public string Description { get; set; } 

        public Enums.Status status { get; set; }

        
    }
}
