using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class CountdownDay
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } // 纪念日名称

        [Required]
        public DateTime TargetDate { get; set; } // 目标日期

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
