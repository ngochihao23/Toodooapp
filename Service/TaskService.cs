using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using THUC_HANH_3.DTOS.Category;
using THUC_HANH_3.DTOS.Tasks;
using THUC_HANH_3.Repository;
using Todoapp.Const;
using Todoapp.Data;
using Todoapp.DTOS.Category;
using Todoapp.DTOS.Tasks;
using Todoapp.Entities;

namespace Todoapp.Service
{
    public class TaskService : ITaskService // bấm Ctrl + > 


    {
        //Bảng task : bảng công việc 
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IResponsitory<TaskEntity> _rpTask;
        private readonly IHttpContextAccessor _contextAccessor;
        public TaskService( AppDbContext context, IMapper mapper, IResponsitory<TaskEntity>taskRepository, IHttpContextAccessor httpContextAccessor  )
        {
            _rpTask = taskRepository;
            _context = context;
            _mapper = mapper;
            _contextAccessor = httpContextAccessor;
        }

        //tạo công việc 
       
        public async Task<Guid> Create(TaskCreateRequest request)
        {
            var taskExist = await _rpTask.AsQueryAble().AnyAsync(t=>t.Tittle==request.Tittle);
            if (taskExist)
            {
                throw new Exception(" Công việc đã tồn tại");
            }    

            //Nếu công việc tồn tại => báo lỗi
            //Vào mapper profile địn nghĩa
            //entity đại diện cho 1 bản ghi <=> 1 dòng dữ liệu trong bảng Tasks
            var entity = _mapper.Map<TaskEntity>(request);

            //thêm etity vào CSDL
            var result = await _rpTask.CreateAsync(entity);
            return entity.Id;
        }
      
       

        //lấy danh sachs tất cả  
        public async Task<List<TaskDto>> GetAll()
        {

            var results = await _rpTask.AsQueryAble()
                .Include(t=>t.Category)
                .Include(t => t.User)
                .Include(t => t.UserCreate).ToListAsync();
            
            
            //Nếu có phần từ trong danh sách 
            return _mapper.Map<List<TaskDto>>(results);
        }

        

        public async Task<bool> Delete ( Guid id )
        {
            var task = await _context.Tasks.FirstOrDefaultAsync (t => t.Id == id);
            if (task == null) 
            {
                throw new Exception("không tìm thấy công việc "); 

            }
            _context.Tasks.Remove(task); 
            await _context.SaveChangesAsync();
            return true; 
        }

        public async Task<List<TaskDto>> GetPaging(TakeGetPaging request)
        {

            var task = _rpTask.AsQueryAble().Include(t=>t.Category).ToList(); //truy vấn còn sử dụng tiếp

            if (!string.IsNullOrEmpty(request.SearchText)) //có thể null :: " ! " làm ngược lại điều kiện
            {
                task = task.Where(t=>t.Tittle.ToUpper().Contains(request.SearchText.ToUpper())).ToList();

            } //xong phần tìm kiếm
            //đếm số lượng phần tử 
            var total = task.Count();
            //Bỏ qua số lượng phần tử 

            task = 
                task.Skip((request.PageIndex -1)*request.PageSize).Take(request.PageSize).ToList();
            await _context.SaveChangesAsync(); 
            return _mapper.Map<List<TaskDto>>(task);

        }


        public async Task<Guid> CreateNew(TaskCreateRequest request)
        {
           
            var Task = await _context.Tasks.AnyAsync(c => c.CategoryId == request.CategoryId); // => Không phải danh mục pHẦN TỬ DANH MỤC 
          
            if (!Task)
            {
                throw new Exception("Danh mục ko tồn tại ");

            }
            
            
            var result = _mapper.Map<Entities.TaskEntity>(request);

            //Thêm entity vào CSDL 
             await _context.Tasks.AddAsync(result);
            //Khi thêm, sửa, xóa => Cần savechange lại
            await _context.SaveChangesAsync();
            return result.Id;
        }

 

       public async Task<Guid> Update(TaskUpdateRequest request)
        {
            var CategoryExist = await _rpTask.GetAsync(request.Id);

            
            // MAPPER 
            var entity = _mapper.Map<TaskEntity>(request);
            // cập nhật nó
            _context.Tasks.Update(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<List<TaskDto>> GetByCategoryId(Guid CategoryId)
        {
            var entity =  _context.Tasks.Where(t => t.CategoryId == CategoryId);
            if (entity == null)
            {
                throw new Exception("không tìm thấy công việc ");

            }
            return (List<TaskDto>)entity;
        }

        public async Task<List<AnalyticsDto>> analytic(AnatalyicRequest request)
        {
            var danhmuc = await _context.Categories.AsQueryable()
                .Include(c => c.Tasks)
                .ToListAsync();

            

            foreach (var d in danhmuc)
            {
                if (request.StartDay.HasValue)
                {
                    d.Tasks = d.Tasks.Where(t => t.CreationDate >= request.StartDay).ToList();
                    
                }
                if (request.EndDay.HasValue)
                {
                    d.Tasks = d.Tasks.Where(t=>t.CreationDate <= request.EndDay).ToList();
                }

                //d.Tasks = _context.Tasks.Where(c=>c.DueTime.);
                //2 if
            }
            return danhmuc.Select(d => new AnalyticsDto
            {
                CategoryName = d.Name,
                Amount = d.Tasks.Count,
                Tasks = d.Tasks
            }).ToList();
        }

        public async Task<List<AnalyticsDto>> analytic(Guid AnatalyicRequest ) 
        {
           
            
                var danhmuc = await _context.Categories.AsQueryable()
                .Include(c=>c.Tasks)
                .ToListAsync(); 

            foreach (var d in danhmuc)
            {
                //d.Tasks = _context.Tasks.Where(c=>c.DueTime.);
            }
            var ketqua = _mapper.Map<List<AnalyticsDto>>(danhmuc);
       
            return ketqua;
            

            //var result = _context.Tasks.Where(c=>c.date >= startDate && c.date <= endDate).ToList(); 



        }


    }
}
