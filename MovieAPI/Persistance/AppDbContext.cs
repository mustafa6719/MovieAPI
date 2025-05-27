using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;

namespace MovieAPI.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Title)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(m => m.Overview)
                      .HasMaxLength(2000);

                entity.Property(m => m.Popularity)
                      .HasPrecision(10, 3); // For large decimal values

                entity.Property(m => m.Vote_Count);

                entity.Property(m => m.Vote_Average)
                      .HasPrecision(3, 1);

                entity.Property(m => m.Original_Language)
                      .HasMaxLength(10);

                entity.Property(m => m.Genre)
                      .HasMaxLength(500); // Comma-separated genres

                entity.Property(m => m.Poster_Url)
                      .HasMaxLength(1000);

                entity.Property(m => m.Release_Date)
                      .HasColumnType("DATE");
            });
        }

    }
}
