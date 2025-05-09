using System.Security.Cryptography;
using System.Text;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<AuthResponse>> Login(LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
                return new ApiResponse<AuthResponse> { Success = false, Message = "User not found" };

            if (!VerifyPasswordHash(request.Password, user.PasswordHash))
                return new ApiResponse<AuthResponse> { Success = false, Message = "Wrong password" };

            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            var Data = new AuthResponse();

            Data.Token = token;
            return new ApiResponse<AuthResponse> { Success = true, Data = Data, Message = "Login successful" };
        }

        public async Task<ApiResponse<AuthResponse>> Register(RegisterRequest request)
        {
            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
                return new ApiResponse<AuthResponse> { Success = false, Message = "Username already exists" };

            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return new ApiResponse<AuthResponse> { Success = false, Message = "Email already exists" };

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = CreatePasswordHash(request.Password),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            Console.Out.WriteLine("用户名:");
            Console.Out.WriteLine(user.CreatedAt);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            var Data = new AuthResponse();

            Data.Token = token;
            return new ApiResponse<AuthResponse> { Success = true, Data = Data, Message = "Registration successful" };
        }

        private string CreatePasswordHash(string password)
        {
            // 使用固定的密钥
            byte[] key = Encoding.UTF8.GetBytes("YourFixedSecretKey123!"); // 替换为你的固定密钥

            using var hmac = new HMACSHA512(key);
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var computedHash = hmac.ComputeHash(passwordBytes);

            // 返回 Base64 编码的字符串，并截取前 255 个字符
            return Convert.ToBase64String(computedHash)[..Math.Min(255, Convert.ToBase64String(computedHash).Length)];
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            // 使用固定的密钥
            byte[] key = Encoding.UTF8.GetBytes("YourFixedSecretKey123!"); // 替换为你的固定密钥

            using var hmac = new HMACSHA512(key);
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var computedHash = hmac.ComputeHash(passwordBytes);

            // 将计算出的哈希值转换为 Base64 编码，并截取前 255 个字符
            var computedHashBase64 = Convert.ToBase64String(computedHash)[..Math.Min(255, Convert.ToBase64String(computedHash).Length)];

            // 比较计算的哈希值和存储的哈希值
            return computedHashBase64 == storedHash;
        }
    }
}