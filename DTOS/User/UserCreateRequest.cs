using Todoapp.Const;

namespace THUC_HANH_3.DTOS.User
{
    public class UserCreateRequest : UserRegisterRequest
    {
        public Enums.Role? Role { get; set; }
    }
}
