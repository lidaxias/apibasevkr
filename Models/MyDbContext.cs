using Microsoft.EntityFrameworkCore;
namespace apibase.Models
{
    public class MyDbContext : DbContext
    { 
        public DbSet<UserModel> Users { get; set; } // таблица 

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
    }
}
