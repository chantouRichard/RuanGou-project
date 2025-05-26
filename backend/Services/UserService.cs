using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<User>> getUserInfo(int id)
        {
            var query = _context.Users.AsQueryable();

            var Data = await query.FirstOrDefaultAsync(t => t.Id == id);

            if (Data == null) return new ApiResponse<User> { Success = false, Data = null, Message = "Get UserInfo By Id failed" };

            return new ApiResponse<User> { Success = true, Data = Data, Message = "Get UserInfo By Id successful" };
        }

        public async Task<ApiResponse<User>> updateUserInfo(User user)
        {
            var result = await getUserInfo(user.Id);
            var existingItem = result.Data;
            if (existingItem == null) return new ApiResponse<User> { Success = false, Data = null, Message = "不存在该用户" };

            existingItem.Email = user.Email;
            existingItem.Nickname = user.Nickname;
            existingItem.AvatarUrl = user.AvatarUrl;
            existingItem.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return new ApiResponse<User> { Success = true, Data = existingItem, Message = "用户信息修改成功" };
        }
    }
}
