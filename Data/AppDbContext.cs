using Microsoft.EntityFrameworkCore;
using SSR_Agile.Models;

namespace SSR_Agile.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}