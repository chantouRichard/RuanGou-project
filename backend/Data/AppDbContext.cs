using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        // 定义 WeatherRecords 数据集
        public DbSet<WeatherRecord> WeatherRecords { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");  // 明确指定表名

                entity.Property(e => e.PasswordHash)
                      .HasColumnName("password_hash"); // 显式指定列名

                entity.Property(e => e.CreatedAt)
                      .HasColumnName("created_at")
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");  // 同步数据库默认值

                entity.Property(e => e.UpdatedAt)
                      .HasColumnName("updated_at")
                      .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<TodoItem>()
                .HasIndex(t => t.UserId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 配置 MySQL 数据库连接字符串（根据你的实际情况修改）
            var connectionString = "server=localhost;port=3306;database=myapidb;user=root;password=123456;";

            Console.Out.WriteLine("控制台报错输出:");

            optionsBuilder
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                // 启用日志，输出到控制台
                .LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}
