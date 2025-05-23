using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class TodoService : ITodoService
    {
        private readonly AppDbContext _context;

        public TodoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IEnumerable<TodoItem>>> GetAllAsync(string userId = null)
        {
            IQueryable<TodoItem> query = _context.TodoItems;

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(t => t.UserId == userId);
            }

            var Data = await query.OrderBy(t => t.DueDate).ToListAsync();

            return new ApiResponse<IEnumerable<TodoItem>> { Success = true, Data = Data, Message = "Get TodoList successful" };
        }

        public async Task<ApiResponse<TodoItem>> GetByIdAsync(int id, string userId = null)
        {
            var query = _context.TodoItems.AsQueryable();

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(t => t.UserId == userId);
            }

            var Data = await query.FirstOrDefaultAsync(t => t.Id == id);

            if (Data == null) return new ApiResponse<TodoItem> { Success = false ,Data = null,Message = "Get TodoList By Id failed" };

            return new ApiResponse<TodoItem> { Success = true, Data = Data, Message = "Get TodoList By Id successful" };
        }

        public async Task<ApiResponse<TodoItem>> CreateAsync(TodoItem item, string userId = null)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                item.UserId = userId;
            }
            item.CreatedAt = DateTime.Now;
            item.UpdatedAt = DateTime.Now;
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return new ApiResponse<TodoItem> { Success = true, Data = item, Message = "Create TodoItem successful" };
        }

        public async Task<ApiResponse<TodoItem>> UpdateAsync(TodoItem item, string userId = null)
        {
            var result = await GetByIdAsync(item.Id, userId);
            var existingItem = result.Data;
            if (existingItem == null) return new ApiResponse<TodoItem> { Success = false, Data = null, Message = "Update TodoItem failed" };

            existingItem.Title = item.Title;
            existingItem.Description = item.Description;
            existingItem.DueDate = item.DueDate;
            existingItem.IsCompleted = item.IsCompleted;
            existingItem.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return new ApiResponse<TodoItem> { Success = true , Data = existingItem, Message = "Update TodoItem successful" };
        }

        public async Task<ApiResponse<TodoItem>> DeleteAsync(int id, string userId = null)
        {
            var result = await GetByIdAsync(id, userId);

            var item = result.Data;

            if (item == null) return new ApiResponse<TodoItem> { Success = false, Data = null, Message = "Delete TodoItem failed" };

            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
            return new ApiResponse<TodoItem> { Success = true, Data = null, Message = "Delete TodoItem successful" };
        }
    }
}