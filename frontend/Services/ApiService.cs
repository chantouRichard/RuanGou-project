using ModernWpf.Controls;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using frontend.Models;

namespace frontend.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5166/api/auth/";

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        public async Task<ApiResponse<AuthToken>> Login(string username, string password)
        {

            try
            {
                var request = new { Username = username, Password = password };
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                Console.Out.WriteLine("测试1");


                var response = await _httpClient.PostAsync("login", content);
                var responseString = await response.Content.ReadAsStringAsync();

                Console.Out.WriteLine("测试2");

                var result = JsonConvert.DeserializeObject<ApiResponse<AuthToken>>(responseString);

                Console.Out.WriteLine(result.Data.Token);


                if (result.Success && result.Data!=null)
                {
                    // 存储 token
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

                var response = await _httpClient.PostAsync("register", content);
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
    }
}