// Models/Geo.cs
namespace backend.Models
{
    public class City
    {
        public required string Adcode { get; set; }
        public required string Name { get; set; }
        public required string Pinyin { get; set; }
    }

    public class Province
    {
        public required string Name { get; set; }
        public List<City> Cities { get; set; } = new();
    }
}