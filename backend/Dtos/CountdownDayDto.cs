namespace backend.Dtos
{
    public class CountdownDayDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime TargetDate { get; set; }

        // 与今天的天数差，正数表示未来天数，负数表示已经过去多少天
        public int DaysDifference { get; set; }
    }

}
