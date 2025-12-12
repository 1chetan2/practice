using firstprogram.Models;
using Microsoft.EntityFrameworkCore;

namespace firstprogram.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Register> Registers { get; set; }
    }
}
