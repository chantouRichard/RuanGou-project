using backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet("recommend")]
        public async Task<IActionResult> GetRandomBook()
        {
            try
            {
                var url = "https://www.googleapis.com/books/v1/volumes?q=subject:fiction&maxResults=40";
                var response = await _httpClient.GetFromJsonAsync<GoogleBooksResponse>(url);

                if (response?.Items == null || response.Items.Count == 0)
                    return NotFound("未找到推荐书籍。");

                var random = new Random();
                var book = response.Items[random.Next(response.Items.Count)];

                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取推荐书籍失败");
                return StatusCode(500, "服务器内部错误");
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("搜索关键词不能为空。");

            try
            {
                var url = $"https://www.googleapis.com/books/v1/volumes?q={Uri.EscapeDataString(query)}";
                var response = await _httpClient.GetFromJsonAsync<GoogleBooksResponse>(url);

                if (response?.Items == null || response.Items.Count == 0)
                    return NotFound("未找到相关书籍。");

                return Ok(response.Items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "搜索书籍失败");
                return StatusCode(500, "服务器内部错误");
            }
        }
    }

}
