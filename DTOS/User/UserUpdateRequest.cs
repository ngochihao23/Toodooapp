using Todoapp.Const;

namespace THUC_HANH_3.DTOS.User
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }

        public string Name { get; set; }

        public Enums.Role? Role { get; set; }


    }
}