using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using backend.Models;
using System.Net.Http.Headers;

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

        public async Task<ImageTranslationResult> ImageTranslateAsync(byte[] imageBytes, string from, string to, int v)
        {
            const string boundary = "----BaiduPictransBoundary";
            try
            {
                // 1. 获取Token
                var token = await GetValidAccessTokenAsync();

                // 2. 构建Multipart请求
                using var content = new MultipartFormDataContent(boundary);

                // 图片部分（关键修正）
                var imageContent = new ByteArrayContent(imageBytes);
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(GetImageMimeType(imageBytes));
                content.Add(imageContent, "image", "translation_image.png");

                // 文本参数（保持小写字段名）
                content.Add(new StringContent(from.ToLower()), "from");
                content.Add(new StringContent(to.ToLower()), "to");
                content.Add(new StringContent(v.ToString()), "v");
                // ============= 调试输出开始 =============
                Console.WriteLine("\n=== MultipartContent 详细信息 ===");
                Console.WriteLine($"Boundary: {content.Headers.ContentType?.Parameters.FirstOrDefault(p => p.Name == "boundary")?.Value}");
                Console.WriteLine($"Content-Type: {content.Headers.ContentType}");

                foreach (var part in content)
                {
                    Console.WriteLine($"\n-- Part: {part.Headers.ContentDisposition?.Name} --");
                    Console.WriteLine($"Headers: {string.Join("; ", part.Headers.Select(h => $"{h.Key}={string.Join(",", h.Value)}"))}");

                    if (part is StringContent stringPart)
                    {
                        Console.WriteLine($"String Content: {await stringPart.ReadAsStringAsync()}");
                    }
                    else if (part is ByteArrayContent bytePart)
                    {
                        var bytes = await bytePart.ReadAsByteArrayAsync();
                        Console.WriteLine($"Byte Length: {bytes.Length}");
                        Console.WriteLine($"Hex Preview: {BitConverter.ToString(bytes.Take(32).ToArray())}...");
                    }
                }
                // ============= 调试输出结束 =============
                // 3. 配置HttpClient
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                // 4. 发送请求
                var response = await httpClient.PostAsync(
                    $"{ImageTranslateUrl}?access_token={token}",
                    content);

                var responseContent = await response.Content.ReadAsStringAsync();

                // 5. 处理响应（严格匹配您的ImageTranslationResult结构）
                var result = JsonConvert.DeserializeObject<ImageTranslationResult>(responseContent);

                if (!response.IsSuccessStatusCode || !string.IsNullOrEmpty(result.ErrorCode))
                {
                    throw new Exception($"翻译失败{(result.ErrorCode != null ? $"[{result.ErrorCode}]" : "")}: {result.ErrorMsg ?? "未知错误"}");
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"图片翻译错误: {ex.Message}\n{ex.StackTrace}");
                throw new Exception("图片翻译服务暂时不可用", ex);
            }
        }

        private string GetImageMimeType(byte[] imageBytes)
        {
            // PNG检测
            if (imageBytes.Length > 8 &&
                imageBytes[0] == 0x89 &&
                imageBytes[1] == 0x50 &&
                imageBytes[2] == 0x4E &&
                imageBytes[3] == 0x47)
            {
                return "image/png";
            }
            // JPEG检测
            if (imageBytes.Length > 2 &&
                imageBytes[0] == 0xFF &&
                imageBytes[1] == 0xD8)
            {
                return "image/jpeg";
            }
            throw new ArgumentException("不支持的图片格式，仅接受PNG或JPEG");
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
