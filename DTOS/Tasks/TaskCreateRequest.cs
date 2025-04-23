using System.ComponentModel.DataAnnotations;
using Todoapp.Const;

namespace Todoapp.DTOS.Tasks
{
    public class TaskCreateRequest
    //DTO : Nhận request từ client dùng để tạo 1 công việc
    {
       
        [Required]

        public string Tittle { get; set; } //BẮT BUỘC NHẬP VÀO
        [Required]

        public string Description { get; set; } //BẮT BUỘC NHẬP VÀO
        [Required]
        public DateTime DueTime { get; set; }

        //Enum : Trạng thái công việc  
        [Required]
        public Enums.Status Status { get; set; }

        public Guid? CategoryId { get; set; }

        public Guid UserId { get; set; }
    }
}
