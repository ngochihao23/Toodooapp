namespace THUC_HANH_3.DTOS.Category
{
    public class CategoryUpdateRequest
    { 
        public Guid Id { get; set; } 
        public string Name { get; set; }  //Tên danh mục
        public string Description { get; set; } // mô tả
    }
}
