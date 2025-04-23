using THUC_HANH_3.DTOS.Tasks;

namespace THUC_HANH_3
{
    public class TaskGetPagingDto
    {
        public List<TaskDto> Items { get; set; }
        public int TotalRecord { get; set; }
    }
}
