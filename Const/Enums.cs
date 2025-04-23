using System.ComponentModel;

namespace Todoapp.Const
{                           //Static : có thể truy cập trực tiếp từ mọi nơi trong projects
    public static class Enums //Kiểu dữ liệu chỉ được sử dụng để đọc, không gán giá trị được 
                        // Kiểu giá trị là 1 hằng số 
    { // Định nghĩa 1 kiểu dữ liệu là trạng thái công việc
        //OOP : đóng gói dữ liệu 2 kiểu : public & private
        public enum Status
        {
            [Description(" Chưa làm")]
            Pending = 0, //chưa làm 
            [Description(" Đang làm")]
            Working = 1, //đamg làm
            [Description(" Hoàn thành")]
            Completed = 2, // đã xong 
        }

        public enum Role
        {
            [Description("User")]
            User = 1,
            [Description("Manager")]
            Manager =2,

            [Description("Admin")]
            Admin = 99
        }
    }
}
