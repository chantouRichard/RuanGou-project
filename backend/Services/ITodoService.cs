using backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Services
{
    public interface ITodoService
    {
        Task<ApiResponse<IEnumerable<TodoItem>>> GetAllAsync(string userId = null);
        Task<ApiResponse<TodoItem>> GetByIdAsync(int id, string userId = null);
        Task<ApiResponse<TodoItem>> CreateAsync(TodoItem item, string userId = null);
        Task<ApiResponse<TodoItem>> UpdateAsync(TodoItem item, string userId = null);
        Task<ApiResponse<TodoItem>> DeleteAsync(int id, string userId = null);
    }
}