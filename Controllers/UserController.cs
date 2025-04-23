using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using THUC_HANH_3.DTOS.User;
using THUC_HANH_3.Service.User;
using Todoapp.DTOS.Tasks;

namespace THUC_HANH_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //1 controller tương ứng với 1 Service <=> 1 đối tượng Entity 
        private readonly IUserService _userService;

        public UserController(IUserService userService) //Đăng Kí DI
        {
            _userService = userService;
        }

        //Đăng kí tài khoản => Api 
        //Truyền dữ liệu thông qua request Body
        // => role phải tiếng anh,
        //IactionResult => trả về 2 loại : 
        // Success : Return Ok
        // Error : => BadRequest => Lỗi 400 của user
        // Tệp => File(data,type,data name )

        //Đăng kí tài khoản
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            try
            {
                var token = await _userService.Register(request);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); //Lỗi từ hệ thống, lỗi từ người dùng,...
            }
        }

        //Đăng nhập tài khoản 
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLogin request)
        {
            try
            {
                var token = await _userService.Login(request);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //Lấy thông tin người dùng thông qua token
        //bắt buộc người dùng phải đăng nhập trước đó 
        // có token thì mới lấy được thông tin
        [HttpPost("Profile")]
        [Authorize] // bắt buộc phải xác thực
        public async Task<IActionResult> Profile()
        {
            try
            {
                var user = await _userService.Profile();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
        {
            try
            {
                var result = await _userService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Getall")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Getall()
        {
            var result = await _userService.GetAll();
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UserUpdateRequest request)
        {
            try
            {

                var result = await _userService.Update(request);
                if (result == null)
                {
                    throw new Exception("Lỗi");

                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    //    [HttpGet("/{id}")]

    //public async Task<IActionResult> GetById( Guid id)
    //    {

    //    }

    }
}
