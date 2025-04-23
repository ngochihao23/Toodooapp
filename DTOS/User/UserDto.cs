using THUC_HANH_3.DTOS.Tasks;
using Todoapp.Const;

namespace THUC_HANH_3.DTOS.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }

        public string Email { get; set; }

        public Enums.Role Role { get; set; }

        public virtual List<TaskDto> Tasks { get; set;}
    }
}
