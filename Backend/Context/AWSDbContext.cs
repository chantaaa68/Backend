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

        public DbSet<Users> Users { get; set; }
        public DbSet<Kakeibo> Kakeibo { get; set; }
        public DbSet<NewsletterTemplate> NewsletterTemplate { get; set; }
        public DbSet<KakeiboItem> KakeiboItem { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryDefault> CategoryDefault { get; set; }
        public DbSet<Icon> Icon { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // joinするときに、delete_date==nullを接続するようにする
            modelBuilder.Entity<Icon>().HasQueryFilter(i => i.DeleteDate == null);
            modelBuilder.Entity<Users>().HasIndex(e => e.Email).IsUnique();
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
