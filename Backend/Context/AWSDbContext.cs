using Backend.Model;
using Backend.Model.Interface;
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

        public DbSet<User> Users { get; set; }
        public DbSet<Kakeibo> Kakeibo { get; set; }
        public DbSet<NewsletterTemplate> NewsletterTemplates { get; set; }
        public DbSet<KakeiboItem> KakeiboItems { get; set; }
        public DbSet<KakeiboItemOption> KakeiboItemOptions { get; set; }
        public DbSet<Category> Kategories { get; set; }
        public DbSet<Icon> Icons { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(e => e.Email).IsUnique();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<KakeiboInterface>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreateDate = DateTime.UtcNow;
                    entry.Entity.UpdateDate = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdateDate = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
