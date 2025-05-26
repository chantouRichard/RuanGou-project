using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using backend.Models;

namespace backend.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "7gQTJ63Rv733T37KOGm5wIqS";
        private const string SecretKey = "gpfbDFezS1D7cWP1SIqrEtTXtv92621s";
        private const string AuthUrl = "https://aip.baidubce.com/oauth/2.0/token";
        private const string TextTranslateUrl = "https://aip.baidubce.com/rpc/2.0/mt/texttrans/v1";
        private const string ImageTranslateUrl = "https://aip.baidubce.com/file/2.0/mt/pictrans/v1";

        private static string _cachedAccessToken;
        private static DateTime _tokenExpiryTime = DateTime.MinValue;

        public TranslationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TextTranslationResult> TextTranslateAsync(string text, string from, string to)
        {
            try
            {
                var token = await GetValidAccessTokenAsync();
                var requestUrl = $"{TextTranslateUrl}?access_token={token}";

                var requestBody = new
                {
                    q = text,
                    from = from,
                    to = to
                };

                var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(requestBody),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync(requestUrl, jsonContent);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"请求失败: {response.StatusCode}\n{responseContent}");
                }

                var result = JsonConvert.DeserializeObject<TextTranslationResult>(responseContent);

                if (result?.Result == null || !string.IsNullOrEmpty(result.ErrorCode))
                {
                    throw new Exception($"翻译失败: {result?.ErrorMsg ?? "未知错误"}");
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"文本翻译异常: {ex}");
                throw;
            }
        }

        public async Task<ImageTranslationResult> ImageTranslateAsync(byte[] imageBytes, string from, string to,int v)
        {
            try
            {
                var token = await GetValidAccessTokenAsync();
                using var content = new MultipartFormDataContent();

                content.Add(new ByteArrayContent(imageBytes), "image", "image.png");
                content.Add(new StringContent(from.ToLower()), "from");
                content.Add(new StringContent(to.ToLower()), "to");
                content.Add(new StringContent(v.ToString()), "v"); // 固定值v=3

                var response = await _httpClient.PostAsync(
                    $"{ImageTranslateUrl}?access_token={token}",
                    content);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"请求失败: {response.StatusCode}\n{responseContent}");
                }

                return JsonConvert.DeserializeObject<ImageTranslationResult>(responseContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"图片翻译异常: {ex}");
                throw;
            }
        }

        private async Task<string> GetValidAccessTokenAsync()
        {
            if (!string.IsNullOrEmpty(_cachedAccessToken) && DateTime.UtcNow < _tokenExpiryTime)
            {
                return _cachedAccessToken;
            }

            try
            {
                var response = await _httpClient.PostAsync(
                    AuthUrl,
                    new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        ["grant_type"] = "client_credentials",
                        ["client_id"] = ApiKey,
                        ["client_secret"] = SecretKey
                    }));

                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"获取Token失败: HTTP {response.StatusCode}\n{responseContent}");
                }

                var result = JsonConvert.DeserializeObject<AccessTokenResult>(responseContent);
                if (result == null || string.IsNullOrEmpty(result.AccessToken))
                {
                    throw new Exception("获取的AccessToken为空");
                }

                _cachedAccessToken = result.AccessToken;
                _tokenExpiryTime = DateTime.UtcNow.AddSeconds(result.ExpiresIn - 300);

                return _cachedAccessToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取AccessToken异常: {ex.Message}");
                throw;
            }
        }
    }
}
