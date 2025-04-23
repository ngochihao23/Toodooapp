using THUC_HANH_3.DTOS.Tasks;

namespace Todoapp.Entities
{
    public class CategoryEntity
    { 
        public Guid Id { get; set; }
        public string Name { get; set; } 

        public string Description { get; set; }

        public virtual List<TaskDto> Tasks { get; set; }

        

        //tìm kiếm theo Index
        // 1 danh mục thì gồm nhiều công việc thuộc danh mục đó : 1-n 
        // 1 công việc thuộc chỉ 1 danh mục . 


    }
}
