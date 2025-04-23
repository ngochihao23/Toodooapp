using System.Runtime.InteropServices;
using THUC_HANH_3.DTOS.Category;
using Todoapp.DTOS.Category;
using Todoapp.Entities;

namespace Todoapp.Service.Category
{
    public interface IcategoryService
    { 
        //Create 
        //Request : cần có name, Description => DTO: CategoryCreateRequest 
        //Response : Id => DTO? => không cần DTO mới 
        Task<Guid> Create (CategoryCreateRequest request);
        Task<Guid> Update(CategoryUpdateRequest request);

        Task<CategoryEntity> Get (Guid Id);
        Task<List<CategoryEntity>> Getall(); 

        Task<CategoryEntity> Delete (Guid Id); 
        Task<CategoryEntity> GetById (Guid Id);

    }

}
