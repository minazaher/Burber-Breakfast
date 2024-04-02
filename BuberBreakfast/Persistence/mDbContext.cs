using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast.Persistence;

public class mDbContext: DbContext
{
    public mDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Breakfast> Breakfasts { get; set; } = null;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Breakfast>(builder =>
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedNever();
            builder.Property(b => b.Name).HasMaxLength(Breakfast.MaxNameLength);
            builder.Property(b => b.Description).HasMaxLength(Breakfast.MaxDescriptionLength);
            builder.Property(b => b.Savory)
                .HasConversion(
                    s => string.Join(",", s),
                    s => s.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
            builder.Property(b => b.Sweet)
                .HasConversion(
                    s => string.Join(",", s),
                    s => s.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
        });
        
    }
}