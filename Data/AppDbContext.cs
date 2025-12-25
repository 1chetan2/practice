using firstprogram.Models;
using firstProgram.Models;
using Microsoft.EntityFrameworkCore;

namespace firstprogram.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<FeesDetail> FeesDetails { get; set; }

    }
}

