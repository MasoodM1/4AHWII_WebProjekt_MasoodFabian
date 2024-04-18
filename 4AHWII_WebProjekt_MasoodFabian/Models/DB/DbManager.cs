using Microsoft.EntityFrameworkCore;

namespace _4AHWII_WebProjekt_MasoodFabian.Models.DB
{
    public class DbManager : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost;database=Web_Projekt;user=root;password=root";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
