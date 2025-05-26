// Services/ICityCodeService.cs
namespace backend.Services
{
    public interface ICityCodeService
    {
        string GetAdcodeByCityName(string cityName);
    }
}