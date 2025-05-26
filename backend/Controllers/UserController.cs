using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ApiResponse<User>> getUserInfo(int id)
        {
            var result = await _userService.getUserInfo(id);

            return result;
        }

        [HttpPut]
        public async Task<ApiResponse<User>> updateUserInfo(User user)
        {
            var result = await _userService.updateUserInfo(user);

            return result;
        }
    }
}
