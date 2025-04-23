using Todoapp.Const;
using Todoapp.Entities;

namespace THUC_HANH_3.DTOS.Tasks
{
    public class TaskDto
    {
        public Guid Id { get; set; } //Guid : global unique indentifier : 128 bit
        public string Tittle { get; set; } // Tên ccong việc
        public string Description { get; set; } //Mô tả
        public DateTime CreationDate { get; set; } //Ngày tạo công việc => auto đặt

        public DateTime DueTime { get; set; }// thời gian hết hạn công việc => Client nhập vào

        //Enum : Trạng thái công việc  
        public Enums.Status Status { get; set; } //trạng thái công việc 

        // Mở rộng bảng danh mục công việc
        public Guid? CategoryId { get; set; } // không bắt buộc
        public string CategoryName { get; set; }
        public virtual CategoryEntity Category { get; set; }
        public string CreatedByUserName { get; set; }
    }
}
