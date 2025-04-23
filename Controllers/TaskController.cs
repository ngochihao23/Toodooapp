using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using THUC_HANH_3.DTOS.Tasks;
using Todoapp.DTOS.Tasks;
using Todoapp.Entities;
using Todoapp.Service;

namespace Todoapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;

        }
        
        [HttpGet("GetAll")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _taskService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] TaskCreateRequest request)
        {
            try
            {
                var result = await _taskService.CreateNew(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] TaskUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _taskService.Update(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }
        [HttpDelete("Delete")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete([FromQuery] Guid request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _taskService.Delete(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Paging")]
        public async Task<IActionResult> Paging([FromBody] TakeGetPaging request)
        {
            try
            {
                var result = await _taskService.GetPaging(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



        }
        [HttpGet("{categoryid}")]
        public async Task<IActionResult> GetByIdCategory()
        {
            try
            {
                var result = await _taskService.GetAll();
                return Ok(result);
      
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("analytic")]
        public async Task<IActionResult> Analytic(AnatalyicRequest request)
        {
            try
            {
                var result = await _taskService.analytic(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //[HttpPost("GETPAGGING")] 
        //public async Task<IActionResult> GetPaging(TakeGetPaging request)
        //{
        //  var  results = await _taskService.GetPaging(request); 
        //    return Ok(results);
        //} 

        

















    }
}
