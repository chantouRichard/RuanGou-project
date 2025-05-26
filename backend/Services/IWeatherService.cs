using backend.Models;

namespace backend.Services
{
    public interface IWeatherService
    {
        Task<WeatherRecord> GetWeatherAsync(string cityCode);
    }
}