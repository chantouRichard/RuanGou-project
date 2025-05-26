using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // 获取当前用户ID
        //private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<TodoItem>>> GetAll(int userId)
        {
            //var userIdStr = HttpContext.Request.Query["userId"].ToString();
            var userIdStr = userId.ToString();

            var result = await _todoService.GetAllAsync(userIdStr);

            return result ;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<TodoItem>> GetById(int id,int userId)
        {
            string userIdStr = userId.ToString();

            var result = await _todoService.GetByIdAsync(id, userIdStr);

            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<TodoItem>> Create([FromBody] TodoItem item,int userId)
        {
            string userIdStr = userId.ToString();

            var result = await _todoService.CreateAsync(item, userIdStr);

            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<TodoItem>> Update(int id, [FromBody] TodoItem item,int userId)
        {

            string userIdStr = userId.ToString();

            item.Id = id;

            var result = await _todoService.UpdateAsync(item, userIdStr);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse<TodoItem>> Delete(int id,int userId)
        {
            string userIdStr = userId.ToString();

            var result = await _todoService.DeleteAsync(id, userIdStr);

            return result;
        }
    }
}