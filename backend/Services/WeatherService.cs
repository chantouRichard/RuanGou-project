using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace backend.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _dbContext;
        private const string ApiKey = "2826c291504234ab02d53a71a3996176"; // 天气API Key
        private const string ApiUrl = "https://restapi.amap.com/v3/weather/weatherInfo";

        public WeatherService(HttpClient httpClient, AppDbContext dbContext)
        {
            _httpClient = httpClient;
            _dbContext = dbContext;
        }

        public async Task<WeatherRecord> GetWeatherAsync(string cityCode)
        {
            // 1. 发起请求
            var url = $"{ApiUrl}?city={cityCode}&key={ApiKey}&extensions=base&output=JSON";
            var response = await _httpClient.GetStringAsync(url);

            // 2. 严格解析JSON
            var json = JObject.Parse(response);

            // 3. 验证状态码
            if (json["status"]?.ToString() != "1")
                throw new Exception($"API错误: {json["info"]}");

            // 4. 安全访问数组
            if (!(json["lives"] is JArray lives) || lives.Count == 0)
                throw new Exception("无有效天气数据");

            var data = lives[0]; // 明确获取数组第一个元素

            // 5. 安全类型转换
            return new WeatherRecord
            {
                City = data["city"]?.ToString(),
                Weather = data["weather"]?.ToString(),
                Temperature = float.Parse(data["temperature"]?.ToString() ?? "0"),
                WindPower = int.Parse((data["windpower"]?.ToString() ?? "0").Replace("≤", "")),
                Humidity = int.Parse(data["humidity"]?.ToString() ?? "0"),
                RecordedAt = DateTime.Parse(data["reporttime"]?.ToString() ?? DateTime.Now.ToString())
            };
        }





    }
}
