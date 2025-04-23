using THUC_HANH_3.Entities;
using Todoapp.Const;

namespace Todoapp.Entities
{
    public class TaskEntity
    {
        internal DateTime date;

        //Đại diện cho 1 bảng CSDL: SQL Sever
        public Guid Id { get; set; } //Guid : global unique indentifier : 128 bit
        public string Tittle { get; set; } // Tên ccong việc
        public string Description { get; set; } //Mô tả
        public DateTime CreationDate { get; set; } //Ngày tạo công việc => auto đặt

        public DateTime DueTime {  get; set; }// thời gian hết hạn công việc => Client nhập vào

        //Enum : Trạng thái công việc  
        public Enums.Status Status { get; set; } //trạng thái công việc 

        // Mở rộng bảng danh mục công việc
        public Guid? CategoryId { get; set; } // không bắt buộc
        public virtual CategoryEntity? Category { get; set; } //không bắt buộc


        //1 cong viec chi thuoc ve 1 nguoi dung 
        //khoa ngoai -> bang user
        public Guid UserId { get; set; }
        // day la Index
        public virtual UserEntity User { get; set; }

        //Nguoi tao cong viec - nguoi giao viec: 

        public Guid CreateById { get; set; } // kHOA NGOAI FK - USER
        public virtual UserEntity UserCreate { get; set; } // KHOA NGOAI FK -USER

    }
}
