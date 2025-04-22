using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResuMate.Shared.Models;

namespace ResuMate.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<GeneratedCv> GeneratedCvs { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        
        builder.Entity<GeneratedCv>()
            .HasKey(c => c.Id);

        builder.Entity<GeneratedCv>()
            .Property(c => c.UserId)
            .IsRequired();
    }
}
