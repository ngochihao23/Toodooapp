using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using THUC_HANH_3.DTOS.Category;
using THUC_HANH_3.Repository;
using Todoapp.Data;
using Todoapp.DTOS.Category;
using Todoapp.Entities;
using Todoapp.Service.Category;

namespace THUC_HANH_3.Service.Category
{
    public class CategoryService : IcategoryService
    {
       
        private readonly IMapper _mapper;
        private readonly IResponsitory<CategoryEntity> _rpCategory; 
        public CategoryService( IMapper mapper, IResponsitory<CategoryEntity>responsitory) //contructor
        {
            
            _mapper = mapper;
            _rpCategory = responsitory;
        }
        public async Task<Guid> Create(CategoryCreateRequest request)
        {
            // tạo danh mục 
            // kiểm tra xem danh mục có trùng tên hay không ? == kiểm tra cứng
            // tân dụng Ctrl + Space 
            var categoryExist = await _rpCategory.AsQueryAble().AnyAsync(c=>c.Name==request.Name); // => Không phải danh mục pHẦN TỬ DANH MỤC 
            //=> Bool : true/false
            //Sử dụng thử Any() => Kiểm tra tồn tại dữ liệu dk? => true  
            //Nếu tồn tại thì biến != null
            if (categoryExist)
            {
                throw new Exception("Danh mục đã tồn tại ");

            }
            // Muốn thêm 1 danh mục thì mình phải có 1 categoryentity 
            // Đang có CategoryCreateRequest => CategoryEntity 

            //Cách 1 
            // Tự Mapper
            //Entity mới từ CategoryCreateRequest 
            var entity = _mapper.Map<CategoryEntity>(request);

            //Thêm entity vào CSDL 
            await _rpCategory.CreateAsync(entity);
            //Khi thêm, sửa, xóa => Cần savechange lại
          
            return entity.Id;
        }

       

        public async Task<CategoryEntity> Get(Guid Id)
        {
            var entity = await _rpCategory.GetAsync(Id);
            return entity;
        }

        public async Task<List<CategoryEntity>> Getall()
        {
            var entity = await _rpCategory.GetAllAsync();
           
            return entity;
        }


        public async Task<Guid> Update(CategoryUpdateRequest request)
        {
            var CategoryExist = await _rpCategory.GetAsync(request.Id);


            // MAPPER 
            var entity = _mapper.Map<CategoryEntity>(request);
            // cập nhật nó
            await _rpCategory.UpdateAsync(entity);
           
            return entity.Id;
        }



        public async Task<CategoryEntity> Delete(Guid Id)
        {
            var entity = await _rpCategory.GetAsync(Id);
            await _rpCategory.DeleteAsync(Id);


            return entity;
        }

        public async Task<CategoryEntity> GetById(Guid Id)
        {
            var entity = await _rpCategory.GetAsync(Id);
            return entity;

        }
    }

}