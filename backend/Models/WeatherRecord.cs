namespace backend.Models
{
    public class WeatherRecord
    {
        public int Id { get; set; }
        public required string City { get; set; } // 存储城市名
        public required string Weather { get; set; } // 天气现象
        public float Temperature { get; set; } // 温度
        public int WindPower { get; set; } // 风力级别
        public int Humidity { get; set; } // 湿度
        public DateTime RecordedAt { get; set; } // 记录时间
    }
}
