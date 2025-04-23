using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using THUC_HANH_3.DTOS.Tasks;
using THUC_HANH_3.DTOS.User;
using THUC_HANH_3.Entities;
using THUC_HANH_3.Repository;
using Todoapp.Entities;

namespace THUC_HANH_3.Service.User
{
    public class UserServicecs : IUserService
    {
        private readonly IResponsitory<UserEntity> _rpUserReponsitory; // gọi thẳng dữ liệu từ User
        private readonly IMapper _mapper; //mapper từ dto sang enity
        private readonly IPasswordHasher<UserEntity> _passwordHasher; //băm mật khẩu
        private readonly IHttpContextAccessor _contextAccessor; //lấy thông tin ng dùng
        private readonly IConfiguration _configuration; // lấy thông tin trong appsettings.jsons


        public UserServicecs(IResponsitory<UserEntity> rpUser,
            IMapper mapper,
            IPasswordHasher<UserEntity> passwordHasher,
            IHttpContextAccessor httpContextAccesor,
            IConfiguration configuration)
        {
            _rpUserReponsitory=rpUser;
            _mapper=mapper;
            _passwordHasher=passwordHasher;
            _contextAccessor=httpContextAccesor;
            _configuration=configuration;

        }

        public async Task<Guid> Create(UserCreateRequest request)
        {
            var userExist = await _rpUserReponsitory.FirstOfDefault(u => u.Email == request.Email);
            if (userExist != null)
            {
                throw new Exception("Đã tồn tại");
            }

            //Khi tìm thấy thì sé mapper
            var enity = _mapper.Map<UserEntity>(request);
            // thiếu mật khẩu, => tự băm mật khẩu và lưu vào entiy
            enity.Password = _passwordHasher.HashPassword(enity, request.Password);
            var result = await _rpUserReponsitory.CreateAsync(enity);
            return result.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _rpUserReponsitory.GetAsync(id);
            await _rpUserReponsitory.DeleteAsync(id);


            return true;
        }

        public async Task<List<UserDto>> GetAll()
        {
            var entity = await _rpUserReponsitory.GetAllAsync();

            return _mapper.Map <List<UserDto>>(entity).ToList();
        }

        public async Task<UserEntity> GetByID(Guid id)
        {
            var entity = await _rpUserReponsitory.GetAsync(id);
            return entity;
        }

        public async Task<string> Login(UserLogin request)
        {
            var user = await _rpUserReponsitory.FirstOfDefault(u => u.Email == request.Email);
            if (user == null)
            {
                throw new Exception(" Người dùng không tồn tại");

            }
            var result = _passwordHasher.VerifyHashedPassword(user,user.Password,request.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception($"Failed to verify {request.Email}");
            }
            return GeneateToken(user);
        }

        public async Task<UserDto> Profile()
        {
            //lấy thông tin token => decode JWT => Cho dữ liệu 
            //String => Guid => chuyển từ string sang guid dùng guid.parse
            var userid = Guid.Parse(_contextAccessor.HttpContext.User.Claims.First(u => u.Type == "Id").Value);
            //tìm thông tin theo Id
            var user = await _rpUserReponsitory.GetAsync(userid);

            //User Enity => Mapper
            return _mapper.Map<UserDto>(user);
        }

        public async Task<string> Register(UserRegisterRequest request)
        {
            var userExist = await _rpUserReponsitory.FirstOfDefault(u=>u.Email == request.Email);
            if(userExist != null)
            {
                throw new Exception("Đã tồn tại");
            }

            //Khi tìm thấy thì sé mapper
            var enity = _mapper.Map<UserEntity>(request);
            // thiếu mật khẩu, => tự băm mật khẩu và lưu vào entiy
            enity.Password = _passwordHasher.HashPassword(enity, request.Password);
            var result = await _rpUserReponsitory.CreateAsync(enity);
            return GeneateToken(result);

        }

        public async Task<Guid> Update(UserUpdateRequest request)
        {
            var UserExist = await _rpUserReponsitory.GetAsync(request.Id);


            // MAPPER 
            var entity = _mapper.Map<UserEntity>(request);

            // cập nhật nó
            return entity.Id;
        }

        private string GeneateToken ( UserEntity user )
        {
            //day la he thong phan quyền Authorize
            //CHỉ đọc được 
            var jwtSettings = _configuration.GetSection("jwtSettings");
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("Role",user.Role.ToString()),
                new Claim("Name",user.Email),
                new Claim("Id",user.Id.ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["Secret"]));
            var creds = new Microsoft.IdentityModel.Tokens.SigningCredentials(key, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
                (
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                 expires: DateTime.Now.AddMonths(1),
                  signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        Task<Guid> IUserService.GetByID(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
