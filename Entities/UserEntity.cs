using Todoapp.Const;
using Todoapp.Entities;

namespace THUC_HANH_3.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }


        public string FullName { get; set; }

        public string Email { get; set; } 

        public string Password { get; set; }

        //Role kh dat la int or string => ma phai dat la enums
        public Enums.Role Role { get; set; }

        //1 nguoi dung co nhieu cong viec
        public List<TaskEntity> Tasks { get; set;}

     


    }
}
