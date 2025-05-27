using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using backend.Models;
using System.Linq;

namespace backend.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly HttpClient _httpClient;

        // 有道翻译配置
        private const string YoudaoAppKey = "40cf4baf117afd3c";
        private const string YoudaoAppSecret = "H3vXAsumdbrMSWmTiEOKp9Tj4MwGlxVL";
        private const string YoudaoImageTranslateUrl = "https://openapi.youdao.com/ocrtransapi";

        // 百度翻译配置
        private const string BaiduApiKey = "7gQTJ63Rv733T37KOGm5wIqS";
        private const string BaiduSecretKey = "gpfbDFezS1D7cWP1SIqrEtTXtv92621s";
        private const string BaiduAuthUrl = "https://aip.baidubce.com/oauth/2.0/token";
        private const string BaiduTextTranslateUrl = "https://aip.baidubce.com/rpc/2.0/mt/texttrans/v1";

        private static string _cachedBaiduAccessToken;
        private static DateTime _baiduTokenExpiryTime = DateTime.MinValue;

        public TranslationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TextTranslationResult> TextTranslateAsync(string text, string from, string to)
        {
            try
            {
                var token = await GetBaiduAccessTokenAsync();
                var requestUrl = $"{BaiduTextTranslateUrl}?access_token={token}";

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

        public async Task<ImageTranslationResult> ImageTranslateAsync(
            byte[] imageBytes,
            string from,
            string to
            )
        {
            const string type = "1";
            const string render = "1";
            try
            {
                // 生成请求参数
                var q = Convert.ToBase64String(imageBytes);
                var salt = Guid.NewGuid().ToString("N");
                var curtime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds().ToString();

                // 计算签名
                var input = q.Length > 20 ?
                    q.Substring(0, 10) + q.Length + q.Substring(q.Length - 10) :
                    q;

                var signStr = YoudaoAppKey + input + salt + curtime + YoudaoAppSecret;
                var sign = ComputeSha256Hash(signStr);

                // 构建请求参数
                var formData = new Dictionary<string, string>
                {
                    {"q", q},
                    {"from", from},
                    {"to", to},
                    {"appKey", YoudaoAppKey},
                    {"salt", salt},
                    {"sign", sign},
                    {"signType", "v3"},
                    {"curtime", curtime},
                    {"type", type},
                    {"render", render},
                    {"docType", "json"}
                };

                // 发送请求
                var content = new FormUrlEncodedContent(formData);
                var response = await _httpClient.PostAsync(YoudaoImageTranslateUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API请求失败: {response.StatusCode}\n{responseContent}");
                }

                var result = JsonConvert.DeserializeObject<ImageTranslationResult>(responseContent);

                if (result.ErrorCode != "0")
                {
                    throw new Exception($"翻译失败: {result.ErrorMsg ?? "未知错误"}");
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"图片翻译错误: {ex.Message}\n{ex.StackTrace}");
                throw new Exception("图片翻译服务暂时不可用", ex);
            }
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private async Task<string> GetBaiduAccessTokenAsync()
        {
            if (!string.IsNullOrEmpty(_cachedBaiduAccessToken) && DateTime.UtcNow < _baiduTokenExpiryTime)
            {
                return _cachedBaiduAccessToken;
            }

            try
            {
                var response = await _httpClient.PostAsync(
                    BaiduAuthUrl,
                    new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        ["grant_type"] = "client_credentials",
                        ["client_id"] = BaiduApiKey,
                        ["client_secret"] = BaiduSecretKey
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

                _cachedBaiduAccessToken = result.AccessToken;
                _baiduTokenExpiryTime = DateTime.UtcNow.AddSeconds(result.ExpiresIn - 300);

                return _cachedBaiduAccessToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取AccessToken异常: {ex.Message}");
                throw;
            }
        }
    }
}
