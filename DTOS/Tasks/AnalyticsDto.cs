namespace THUC_HANH_3.DTOS.Tasks
{
    public class AnalyticsDto
    { 
        public string CategoryName { get; set; } 

        public int Amount { get; set; } 

        public List<TaskDto> Tasks { get; set; }
    }

}
