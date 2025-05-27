using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using frontend.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Diagnostics;

namespace frontend.Services
{
    public interface ITranslationService
    {
        Task<TextTranslationResult> TranslateTextAsync(TextTranslationRequest request);
        Task<ImageTranslationResult> TranslateImageAsync(ImageTranslationRequest request);
    }

    public class TranslationService : ITranslationService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5166/api/Translation/";

        public TranslationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<TextTranslationResult> TranslateTextAsync(TextTranslationRequest request)
        {
            try
            {
                Debug.WriteLine($"Sending translation request: {JsonConvert.SerializeObject(request)}");

                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("text", content);
                var responseString = await response.Content.ReadAsStringAsync();

                Debug.WriteLine($"Received translation response: {responseString}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API请求失败: {response.StatusCode}");
                }

                var result = JsonConvert.DeserializeObject<TextTranslationResult>(responseString);

                if (result == null)
                {
                    throw new Exception("反序列化响应失败");
                }

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Translation error: {ex}");
                throw;
            }
        }

        public async Task<ImageTranslationResult> TranslateImageAsync(ImageTranslationRequest request)
        {
            try
            {
                // 添加请求日志
                Debug.WriteLine($"请求参数: From={request.From}, To={request.To}, ImageSize={request.ImageBytes.Length}");

                using var content = new MultipartFormDataContent();
                content.Add(new ByteArrayContent(request.ImageBytes), "image", "screenshot.png");
                content.Add(new StringContent(request.From), "from");
                content.Add(new StringContent(request.To), "to");

                var response = await _httpClient.PostAsync("image", content);
                var responseString = await response.Content.ReadAsStringAsync();

                // 关键调试日志
                Debug.WriteLine($"API原始响应: {responseString}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"HTTP {response.StatusCode}");
                }

                var result = JsonConvert.DeserializeObject<ImageTranslationResult>(responseString);
                return result ?? throw new Exception("反序列化结果为null");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"完整错误: {ex}");
                throw;
            }
        }

    }
}
