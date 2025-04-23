using THUC_HANH_3.DTOS.Tasks;
using THUC_HANH_3.DTOS.User;
using Todoapp.DTOS.Tasks;

namespace THUC_HANH_3.Service.User
{
    public interface IUserService
    {
        //Viết Resgiter - Đăng kí => Tạo ra 1 dòng dữ liệu trong bảng Users 
        // cầm có UserEntity 
        // Cần có người đùng cung cập thoobng tin qua 1 dto -> Usẻ ResgisterRequest 
        // Login 
        //Be : Nhận Dto trên sau đó mapper sang UserEntity 
        //Trả về 1 token của người dùng dạng string.
        Task<string> Register(UserRegisterRequest request);
        //Login
        //CẦN CÓ EMAIL + PASSWORK 
        // TRẢ VỀ TOKEN

        //Logic đăng nhập
        Task<string> Login(UserLogin request);
        //Lấy thông tin người dùng qua token 
        // không cần dữ liệu đầu vào, chỉ cần người dùng đăng nhập thành công và đính kèm token
        //trả về 1 DTO => USERDto
        Task<UserDto> Profile();

        //Des: flow 
        //1. Đăng Ký : người dùng cung cấp thông tin cá nhân thông qua UserRegisterRequest
        // 2. BE nhận UserRegister, kiêm tra username có bị trungfk hông -> nếu có thì --> lỗi , không thì mapper sang <UserEntity> 
        //Băm mật khaair người dùng => Luwu CSDL . Hash mật khẩu nó => Dùng IpasswordHasher. tự đông là Users


        //2 trạng thái : ĐĂNG NHẬP 
        // Người dùng cung cấp Email + Password thông qua UserLoginRequest 
        // Tìm được ng dùng trong CSDL qua emnail 
        // NEUS CÓ THÌ SO SANH MẬT KHẨU NGƯỜI DÙNG VỚI MẬT KHẨU TRONG CSDL
        Task<List<UserDto>> GetAll();
        Task<Guid> Create(UserCreateRequest request); //Tạo công việc,khi công việc được tạo thành công => trả về 1 ID của công việc đó

        Task<Guid> Update(UserUpdateRequest request); //cập nhật công việc 
        Task<bool> Delete(Guid id);

        Task<Guid> GetByID(Guid id);


    }
}
