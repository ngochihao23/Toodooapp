using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using THUC_HANH_3.DTOS.Tasks;
using Todoapp.DTOS.Tasks;
using Todoapp.Entities;

namespace Todoapp.Service
{
    public interface ITaskService // ctrl + >
        //interface : lớp dùng để thể hiện các hành động của 1 đối tượng 
        // C R U D : Create Read Updatwe Delete 
    {
        //Task : hành động công việc : hành động trong hệ thộng
        Task<List<TaskDto>> GetAll();
        Task<Guid> Create(TaskCreateRequest request); //Tạo công việc,khi công việc được tạo thành công => trả về 1 ID của công việc đó

        Task<Guid> Update(TaskUpdateRequest request); //cập nhật công việc 
        Task<bool> Delete(Guid id); 

        Task<List<TaskDto>>  GetPaging(TakeGetPaging request); // Phân trang

        Task<Guid> CreateNew(TaskCreateRequest request);

        Task<List<TaskDto>>GetByCategoryId(Guid CategoryId); 

        Task<List<AnalyticsDto>>analytic(AnatalyicRequest request);
       
    }
}
