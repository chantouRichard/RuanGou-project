// WeatherService.cs
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace frontend.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5166/api/"; // 指向API根路径

        public WeatherService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        // 获取城市编码
        public async Task<string> GetCityCodeAsync(string cityName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"City/adcode?city={WebUtility.UrlEncode(cityName)}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取城市编码失败: {ex.Message}");
                return null;
            }
        }

        // 获取天气数据
        public async Task<string> GetWeatherAsync(string cityCode)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Weather?cityCode={WebUtility.UrlEncode(cityCode)}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取天气失败: {ex.Message}");
                return null;
            }
        }
    }
}
