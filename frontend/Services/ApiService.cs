using ModernWpf.Controls;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using frontend.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Reflection.Metadata;

namespace frontend.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5166/api/";

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        #region 登录注册相关方法
        public async Task<ApiResponse<AuthToken>> Login(string username, string password)
        {

            try
            {
                var request = new { Username = username, Password = password };
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                Console.Out.WriteLine("测试1");


                var response = await _httpClient.PostAsync("auth/login", content);
                var responseString = await response.Content.ReadAsStringAsync();

                Console.Out.WriteLine("测试2");

                var result = JsonConvert.DeserializeObject<ApiResponse<AuthToken>>(responseString);

                Console.Out.WriteLine(result.Data.Token);


                if (result.Success && result.Data != null)
                {
                    // 存储 token
                    Properties.Settings.Default.UserId = result.Data.userId;
                    Properties.Settings.Default.AuthToken = result.Data.Token;
                    Properties.Settings.Default.Save();

                    // 读取 token（在其他地方使用时）
                    var token = Properties.Settings.Default.AuthToken;
                }

                return result;
            }
            catch (Exception ex)
            {
                return new ApiResponse<AuthToken>
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<AuthToken>> Register(string username, string email, string password)
        {
            try
            {
                var request = new { Username = username, Email = email, Password = password };
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("auth/register", content);
                var responseString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ApiResponse<AuthToken>>(responseString);

                if (result.Success)
                {
                    // Store token in application settings
                    Properties.Settings.Default.AuthToken = result.Data.Token;
                    Properties.Settings.Default.Save();
                }

                return result;
            }
            catch (Exception ex)
            {
                return new ApiResponse<AuthToken>
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
        #endregion

        #region 待办事项相关方法
        public async Task<ApiResponse<List<TodoItem>>> GetAllTodos()
        {
            Console.WriteLine($"获取到的数据: {Properties.Settings.Default.UserId}");

            var response = await _httpClient.GetAsync($"todo?userId={Properties.Settings.Default.UserId}");
            response.EnsureSuccessStatusCode();

            Console.WriteLine($"获取到的数据: {response}");

            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"响应内容: {responseString}");

            return JsonConvert.DeserializeObject<ApiResponse<List<TodoItem>>>(responseString);
        }

        public async Task<ApiResponse<TodoItem>> GetTodoById(int id)
        {
            var response = await _httpClient.GetAsync($"todo/{id}?userId=" + Properties.Settings.Default.UserId);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<TodoItem>>(responseString);
        }

        public async Task<ApiResponse<TodoItem>> CreateTodo(TodoItem item)
        {
            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"todo?userId={Properties.Settings.Default.UserId}", content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<TodoItem>>(responseString);
        }

        public async Task<ApiResponse<TodoItem>> UpdateTodo(TodoItem item)
        {
            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"todo/{item.Id}?userId={Properties.Settings.Default.UserId}", content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<TodoItem>>(responseString);
        }

        public async Task<ApiResponse<TodoItem>> DeleteTodo(int id)
        {
            var response = await _httpClient.DeleteAsync($"todo/{id}?userId=" + Properties.Settings.Default.UserId);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<TodoItem>>(responseString);
        }
        #endregion

        #region 用户相关接口

        public async Task<ApiResponse<User>> getUserInfo()
        {
            try
            {
                var response = await _httpClient.GetAsync($"user?userId={Properties.Settings.Default.UserId}");
                var responseString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ApiResponse<User>>(responseString);

                Console.Out.WriteLine(result.Data.Username);

                return result;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error:", e);

                return new ApiResponse<User>
                {
                    Success = false,
                    Message = $"An error occurred: {e.Message}"
                };
            }
        }

        public async Task<ApiResponse<User>> updateUser(User user)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"user?userId={Properties.Settings.Default.UserId}", content);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ApiResponse<User>>(responseString);
            }catch(Exception e)
            {
                return new ApiResponse<User>
                {
                    Success = false,
                    Message = $"An error occurred: {e.Message}"
                };
            }
        }

        #endregion
    }
}