using backend.Models;
using System.Threading.Tasks;

namespace backend.Services
{
    public interface ITranslationService
    {
        Task<TextTranslationResult> TextTranslateAsync(string text, string from, string to);
        Task<ImageTranslationResult> ImageTranslateAsync(byte[] imageBytes, string from, string to,int v);
    }
}
