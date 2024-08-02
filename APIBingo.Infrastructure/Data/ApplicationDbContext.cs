using APIBingo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIBingo.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<BingoNumber> BingoNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BingoNumber>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);
        }
    }
}
