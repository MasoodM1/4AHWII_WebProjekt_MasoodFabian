using Microsoft.EntityFrameworkCore;

namespace _4AHWII_WebProjekt_MasoodFabian.Models.DB
{
    public class DbManager : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BlogPost> Posts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost;database=web_projekt;user=root;password=root";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
