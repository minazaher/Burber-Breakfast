using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast.Persistence;

public class mDbContext: IdentityDbContext
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

        modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(e => e.UserId);
        modelBuilder.Entity<IdentityUserRole<string>>().HasKey(e => e.UserId);
        modelBuilder.Entity<IdentityUserToken<string>>().HasKey(e => e.UserId);


    }
}