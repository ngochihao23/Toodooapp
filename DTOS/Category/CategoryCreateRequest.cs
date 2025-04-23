
namespace Todoapp.DTOS.Category
{
    public class CategoryCreateRequest
    {
        public string Name { get; set; }  //Tên danh mục
        public string Description { get; set; } // mô tả
        public Guid? CategoryId { get; internal set; }
    }
}
