using Backend.Model;
using Microsoft.EntityFrameworkCore;
using WebApplication.Model;

namespace WebApplication.Context
{
    public class AWSDbContext : DbContext
    {
        public AWSDbContext()
        {
        }

        public AWSDbContext(DbContextOptions<AWSDbContext> options) : base(options) { }

        public DbSet<UserData> UserDatas { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserData>().HasIndex(e => e.Email).IsUnique();
        }

    }
}
