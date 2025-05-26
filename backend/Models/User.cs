using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string ?Username { get; set; }

        public string ?Nickname { get; set; }
        public string ?Email { get; set; }

        [Column("password_hash")]
        public string ?PasswordHash { get; set; }

        [Column("created_at")]  // 明确指定数据库列名
        public DateTime? CreatedAt { get; set; }  // 注意使用Nullable<DateTime>匹配数据库的NULL允许

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // 新增头像字段：保存图片路径或URL
        [Column("avatar_url")]
        public string? AvatarUrl { get; set; }
    }
}