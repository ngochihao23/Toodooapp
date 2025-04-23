using AutoMapper;
using THUC_HANH_3.DTOS.Category;
using THUC_HANH_3.DTOS.Tasks;
using THUC_HANH_3.DTOS.User;
using THUC_HANH_3.Entities;
using Todoapp.Const;
using Todoapp.DTOS.Category;
using Todoapp.DTOS.Tasks;
using Todoapp.Entities;

namespace THUC_HANH_3.Mapper
{

    //Để biết được nơi khai báo Mapper 
    // Kế thừa profile 
    public class MappingProfile : Profile
    {
        //Mapper tại đây 
        public MappingProfile()
        {
            //chuyển CategoryCreateRequest sang => CategoryENitity 
            CreateMap<CategoryCreateRequest, CategoryEntity>() ; 
            
            // chuyển từ update sang Enity
            CreateMap<CategoryUpdateRequest, CategoryEntity>() ;

            //chuyển từ Create & Update sang TaskEntiy 
            CreateMap<TaskCreateRequest, TaskEntity>()
                .ForMember(dest => dest.Status, o => o.MapFrom(src => Enums.Status.Pending))
                .ForMember(dest => dest.CreationDate, o => o.MapFrom(src => System.DateTime.Now))
                .ForMember(dest => dest.CreateById, o => o.Ignore());
           
            //chuyển TaskEnity sang TaskDto 
            CreateMap<TaskEntity, TaskDto>()
            .ForMember(dest => dest.CategoryName, o => o.MapFrom(src => src.Category != null ? src.Category.Name : "Khác"))
            .ForMember(dest => dest.CategoryName, o => o.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.CreatedByUserName, o => o.MapFrom(src => src.UserCreate.FullName));

            //update
            CreateMap<TaskUpdateRequest, TaskEntity>()
               .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // src = CategoryEntity, des = AnalyticsDto,
            CreateMap<CategoryEntity,AnalyticsDto>()
                .ForMember(des=>des.CategoryName, o => o.MapFrom(src => src.Name))
                .ForMember(des=>des.Amount, o => o.MapFrom(src => src.Tasks.Count())); 

            // danh mục sang AnalyticsDto 
            CreateMap<TaskDto,AnalyticsDto > ();

            //định nghĩa TaskCreaterequest sang taskentity
            CreateMap<TaskCreateRequest, TaskEntity>()
                .ForMember(dest => dest.CreationDate, o => o.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Status, o => o.MapFrom(src => Enums.Status.Pending));


            CreateMap<TaskUpdateRequest, TaskEntity>();
            


            CreateMap<UserCreateRequest, UserEntity>()
                .ForMember(dest => dest.Role, o => o.MapFrom(src => Enums.Role.User))
            // Không Mapper mật khẩu => Xai ignore
                .ForMember(dest => dest.Password, o => o.Ignore());
            CreateMap<UserEntity, UserDto>();

            CreateMap<UserRegisterRequest, UserEntity>();

        }

    }
}
