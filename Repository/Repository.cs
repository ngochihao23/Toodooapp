using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq.Expressions;
using Todoapp.Data;

namespace THUC_HANH_3.Repository
{
    public class Repository<TEntity> : IResponsitory<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context; //Dùng để kết nối CSDL-Database
        private readonly DbSet<TEntity> _dbSet; // Dùng để kết nối với bảng


        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> AsQueryAble()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
           var result =_dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task CreateListAsync(List<TEntity> entities)
        {
            //Danh sách đối tượng cần tạo
            await _dbSet.AddRangeAsync(entities); 
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid Id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => EF.Property<Guid>(x, "Id") == Id);
            if (result == null)
            {
                throw new Exception("ko tìm thấy đối tượng");
            } 
            _dbSet.Remove(result);
            await _context.SaveChangesAsync();
           
        }

        public async Task DeleteListAsync(List<Guid> Ids)
        {
            //Danh sách đối tượng cần xóa => tìm theo danh sách Id
            var result = await _dbSet.Where(x => Ids.Contains(EF.Property<Guid>(x, "Id"))).ToListAsync();
            if (result.Count == 0)
            {
                throw new Exception("Khong tim thay doi tuong");
            }
            _dbSet.RemoveRange(result);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await _dbSet.Where(predicate).ToListAsync();
            if (result.Count == 0)
            {
                throw new Exception("Khong Có Dữ Liệu");

            }
            return result;
        }

        public async Task<TEntity> FirstOfDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await _dbSet.FirstOrDefaultAsync(predicate);
           
            return result;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var result =  _dbSet.ToList(); 
            if (result.Count == 0) 
            {
                throw new Exception("Khong Có Dữ Liệu");

            }
            return result;
        }

        public async Task<TEntity?> GetAsync(Guid Id)
        {
            // Lấy đối tượng theo id
            var result = await _dbSet.FirstOrDefaultAsync(x => EF.Property<Guid>(x, "Id") == Id);
            if (result == null)
            {
                throw new Exception("ko tìm thấy đối tượng");
            }
            return result;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var result =  _dbSet.Update(entity);
            return result.Entity;
            
          
        }
    }

}
