namespace frontend.Models
{
    public class WeatherRecord
    {
        public  required string City { get; set; }
        public required string Weather { get; set; }
        public float Temperature { get; set; }
        public int WindPower { get; set; }
        public int Humidity { get; set; }
        public DateTime RecordedAt { get; set; }
    }
}
