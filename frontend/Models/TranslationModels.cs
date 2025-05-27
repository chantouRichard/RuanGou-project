using System.Collections.Generic;
using Newtonsoft.Json;

namespace frontend.Models
{
    // 文本翻译请求（保持不变）
    public class TextTranslationRequest
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }
    }

    // 文本翻译结果（保持不变）
    public class TextTranslationResult
    {
        [JsonProperty("logId")]
        public long LogId { get; set; }

        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty("errorMsg")]
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

        [JsonProperty("transResult")]
        public List<TranslationItem> TransResult { get; set; }
    }

    public class TranslationItem
    {
        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("dst")]
        public string Dst { get; set; }
    }

    // 图片翻译请求（修改为与后端对齐）
    public class ImageTranslationRequest
    {
        public byte[] ImageBytes { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }
    }

    // 图片翻译结果（修改为与后端对齐）
    // 修正属性命名以严格匹配有道API响应
    public class ImageTranslationResult
    {
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

        // 修正属性名称为errorMsg（注意小写）
        [JsonProperty("errorMsg")]
        public string ErrorMsg { get; set; }

        [JsonProperty("orientation")]
        public string Orientation { get; set; }

        [JsonProperty("lanFrom")]
        public string SourceLanguage { get; set; }

        [JsonProperty("lanTo")]
        public string TargetLanguage { get; set; }

        [JsonProperty("resRegions")]
        public List<TextRegion> Regions { get; set; }

        public class TextRegion
        {
            [JsonProperty("boundingBox")]
            public string BoundingBox { get; set; }

            [JsonProperty("context")]
            public string OriginalText { get; set; }

            [JsonProperty("tranContent")]
            public string TranslatedText { get; set; }
        }
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
