using Microsoft.EntityFrameworkCore;
using THUC_HANH_3.Entities;
using Todoapp.Entities;

namespace Todoapp.Data
{
    public class AppDbContext : DbContext
    {
        //Đaay là nơi chứa các bảng trong CSDL 
        //Db set: đại diệm 1 bảng trong CSDL 
        // Tạo Hàm xây dựng 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        //Khai báo bảng 
        public DbSet<TaskEntity> Tasks { get; set; }     // Tên bảng : Tasks 
        //TasksEntity : đối tượng mình khai báo bằng code c# => tên bảng trong CDSL

        public DbSet<CategoryEntity> Categories { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        //Đặt quan hệ bảng 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // mqh 1 - nhiều Category ENTITY VÀ TaskEntity
            modelBuilder.Entity<CategoryEntity>()
                .HasMany(c => c.Tasks)    //1 - n
                .WithOne(t => t.Category) //1 task - 1 category
                .HasForeignKey(t => t.CategoryId) // Lấy CategoryId của TaskEntity FK CategoryEntity
                .OnDelete(DeleteBehavior.SetNull);


            //    // Cascade : Xóa Category đồng nghĩa xóa hết tasks
            //    // SetNull : Xóa Category thì CategoryId của TaskEntity = null 
            //    //NoAction : Không Làm gì

         




            //Quan he 1- n UserEnity va TaskEntity
            modelBuilder.Entity<UserEntity>()
                .HasMany(u=>u.Tasks) //1-n Task
                .WithOne(t => t.User) //1Task - 1 User
                .HasForeignKey(t=>t.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            //}   

            modelBuilder.Entity<TaskEntity>()
               .HasOne(u => u.UserCreate) //1-n Task
               .WithMany() //1Task - 1 User
               .HasForeignKey(t => t.CreateById)
               .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder); 

        }
    }
}
