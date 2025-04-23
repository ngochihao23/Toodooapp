using Todoapp.Const;
using Todoapp.Entities;

namespace THUC_HANH_3.DTOS.User
{
    public class UserRegisterRequest
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }


    }
}
