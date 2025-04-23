using System.Linq.Expressions;

namespace THUC_HANH_3.Repository
{
    public interface IResponsitory<TEntity>where TEntity : class
    {
        IQueryable<TEntity> AsQueryAble(); // Hành động truy vấn tự do, gồm nhiều thao tác, tự truy vấn
        // Logic tái sử dụng hành động.

        //Create 
        Task<TEntity> CreateAsync(TEntity entity);

        //Update : cập nhật 
        Task<TEntity> UpdateAsync(TEntity entity);

        //Delete : xóa 
        Task DeleteAsync(Guid Id); 

        // xóa nhiều đối tượng cùng lúc 
        Task DeleteListAsync(List<Guid>Ids);

        //get/ getall
        Task<TEntity?> GetAsync(Guid Id);
        Task<List<TEntity>> GetAllAsync();

        //Tạo nhiều đối tượng cùng một lúc 
        Task CreateListAsync ( List<TEntity> entities );
        //LẤY 1 ĐỐI TƯỢNG THEO ĐIỀU KIỆN
        Task<TEntity> FirstOfDefault(Expression<Func<TEntity, bool>>predicate);
        //LẤY Nhiều ĐỐI TƯỢNG THEO ĐIỀU KIỆN 
        Task<List<TEntity>>FindAsync(Expression<Func<TEntity, bool>>predicate);
    }
}
