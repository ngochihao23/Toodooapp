using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using THUC_HANH_3.DTOS.Category;
using Todoapp.DTOS.Category;
using Todoapp.Service.Category;

namespace THUC_HANH_3.Controllers
{
    [Route("api/[controller]")] // Đây là định tuyến endpoint => api/Category
    [ApiController]
    public class CategoryController : ControllerBase
    {
        //Để sử dụng các hành động vừa định nghĩa ==> phải gọi interface
        private readonly IcategoryService _icategoryService;
        public CategoryController(IcategoryService categoryService)
        {
            _icategoryService = categoryService;
        } 
        //Định nghĩa các hành động => endpoint 

        //Tạo danh mục : Endpoinr
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CategoryCreateRequest request)
        {
            try
            {
                var result = await _icategoryService.Create(request);
                return Ok(result);
            } 
            catch (Exception ex) //Bắt lỗi
            {
                return BadRequest(ex.Message);
            } 

        }
        [HttpPut("update")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Update (CategoryUpdateRequest request)
        {
            try
            {        //bất đồng bộ nên sử dụng async và await 
                var result = await _icategoryService.Update(request);
                    return Ok(result); 
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Getall")]
        public async Task<IActionResult> GetAll() 
        {
            try
            {
                var result = await _icategoryService.Getall(); 
                return Ok(result);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("id")] 
        public async Task<IActionResult> Get(Guid Id)
        {
            try
            {
                var result = await _icategoryService.Get(Id); 
                return Ok(result);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("id")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                var result = await _icategoryService.Delete(Id);  
                return BadRequest(result);
            } 
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get/{Id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            try
            {
                var result = await _icategoryService.GetById(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
















    }
}
