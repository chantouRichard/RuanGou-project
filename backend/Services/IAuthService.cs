using backend.Models;

namespace backend.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<AuthResponse>> Login(LoginRequest request);
        Task<ApiResponse<AuthResponse>> Register(RegisterRequest request);
    }
}