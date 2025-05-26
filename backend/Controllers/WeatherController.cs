//WeatherController.cs
using Microsoft.AspNetCore.Mvc;
using backend.Services;
using backend.Models;
using System.ComponentModel.DataAnnotations;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // 更符合RESTful规范
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService; // 改为接口注入

        // 修正点1：通过接口注入服务
        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        // 修正点2：添加参数验证
        [HttpGet]
        public async Task<IActionResult> GetWeather([FromQuery] string cityCode)
        {
            try
            {
                var weather = await _weatherService.GetWeatherAsync(cityCode);
                return Ok(new
                {
                    status = "1",
                    city = weather.City,
                    weather = weather.Weather,
                    temperature = weather.Temperature,
                    windpower = weather.WindPower,
                    humidity = weather.Humidity,
                    reporttime = weather.RecordedAt.ToString("yyyy-MM-dd HH:mm:ss")
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = "0",
                    info = ex.Message,
                    infocode = "50000"
                });
            }
        }


    }

    // 修正点5：添加响应DTO
    public record WeatherResponseDto(
        string City,
        string Weather,
        float Temperature,
        int WindPower,
        int Humidity,
        DateTime RecordedAt
    );
}