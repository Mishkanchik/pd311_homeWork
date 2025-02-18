using Microsoft.EntityFrameworkCore;
using pd311_mvc_aspnet.Models;

namespace pd311_mvc_aspnet.Data
{
    public class AppDbContext(DbContextOptions options) 
        : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
