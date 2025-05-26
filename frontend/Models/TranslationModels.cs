using System.Collections.Generic;
using Newtonsoft.Json;

namespace frontend.Models
{
    // 文本翻译请求
    public class TextTranslationRequest
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }
    }

    // 修正后的文本翻译结果
    public class TextTranslationResult
    {
        [JsonProperty("logId")]  // 与后端返回一致
        public long LogId { get; set; }

        [JsonProperty("errorCode")]  // 修正大小写
        public string ErrorCode { get; set; }

        [JsonProperty("errorMsg")]  // 修正大小写
        public string ErrorMsg { get; set; }

        [JsonProperty("result")]
        public TextTranslationData Result { get; set; }
    }

    public class TextTranslationData
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("transResult")]  // 修正大小写
        public List<TranslationItem> TransResult { get; set; }
    }

    public class TranslationItem
    {
        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("dst")]
        public string Dst { get; set; }
    }

    // 图片翻译请求
    public class ImageTranslationRequest
    {
        public byte[] ImageBytes { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("v")]
        public int V { get; set; } = 3;
    }

    // 图片翻译结果
    public class ImageTranslationResult
    {
        [JsonProperty("errorCode")]  // 修正大小写
        public string ErrorCode { get; set; }

        [JsonProperty("errorMsg")]  // 修正大小写
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
