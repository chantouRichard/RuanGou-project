using System.Collections.Generic;
using Newtonsoft.Json;

namespace backend.Models
{
    public class TextTranslationRequest
    {
        public required string Text { get; set; }
        public required string From { get; set; }
        public required string To { get; set; }
    }

    public class ImageTranslationRequest
    {
        public required IFormFile image { get; set; }
        public required string from { get; set; }
        public required string to { get; set; }
        public int v { get; set; } = 3;

    }

    public class AccessTokenResult
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }

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

    public class TextTranslationData
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("trans_result")]
        public List<TranslationItem> TransResult { get; set; }
    }

    public class ImageTranslationResult
    {
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        [JsonProperty("error_msg")]
        public string ErrorMsg { get; set; }

        [JsonProperty("data")]
        public ImageTranslationData Data { get; set; }
    }

    public class ImageTranslationData
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("content")]
        public List<ImageTranslationContent> Content { get; set; }

        [JsonProperty("sumSrc")]
        public string SumSrc { get; set; }

        [JsonProperty("sumDst")]
        public string SumDst { get; set; }
    }

    public class TranslationItem
    {
        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("dst")]
        public string Dst { get; set; }
    }

    public class ImageTranslationContent
    {
        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("dst")]
        public string Dst { get; set; }

        [JsonProperty("rect")]
        public string Rect { get; set; }

        [JsonProperty("points")]
        public List<Point> Points { get; set; }
    }

    public class Point
    {
        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }
    }
}
