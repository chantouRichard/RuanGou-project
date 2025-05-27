using System.Collections.Generic;
using Newtonsoft.Json;

namespace backend.Models
{
    // 文本翻译请求保持不变
    public class TextTranslationRequest
    {
        public required string Text { get; set; }
        public required string From { get; set; }
        public required string To { get; set; }
    }

    // 图片翻译请求
    public class ImageTranslationRequest
    {
        public required IFormFile Image { get; set; }
        public required string From { get; set; }
        public required string To { get; set; }
        //public string Type { get; set; } = "1";
        //public string Render { get; set; } = "0";
    }

    // 百度AccessToken结果
    public class AccessTokenResult
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }

    // 文本翻译结果
    public class TextTranslationResult
    {
        [JsonProperty("log_id")]
        public long LogId { get; set; }

        [JsonProperty("result")]
        public TextTranslationData Result { get; set; }

        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        [JsonProperty("error_msg")]
        public string ErrorMsg { get; set; }
    }

    // 图片翻译结果（有道格式）
    public class ImageTranslationResult
    {
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty("errorMsg")]
        public string ErrorMsg { get; set; }

        [JsonProperty("orientation")]
        public string Orientation { get; set; }

        [JsonProperty("lanFrom")]
        public string LanFrom { get; set; }

        [JsonProperty("lanTo")]
        public string LanTo { get; set; }

        [JsonProperty("resRegions")]
        public List<TranslationRegion> ResRegions { get; set; }
    }

    public class TextTranslationData
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("trans_result")]
        public List<TranslationItem> TransResult { get; set; }
    }

    public class TranslationItem
    {
        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("dst")]
        public string Dst { get; set; }
    }

    public class TranslationRegion
    {
        [JsonProperty("boundingBox")]
        public string BoundingBox { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }

        [JsonProperty("tranContent")]
        public string TranContent { get; set; }
    }
}
