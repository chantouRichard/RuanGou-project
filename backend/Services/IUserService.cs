using backend.Models;

namespace backend.Services
{
    public interface IUserService
    {
        Task<ApiResponse<User>> getUserInfo(int id);

        Task<ApiResponse<User>> updateUserInfo(User user);
    }
}
