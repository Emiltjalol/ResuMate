using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResuMate.Shared.Models;

namespace ResuMate.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<GeneratedCv> GeneratedCvs { get; set; } = default!;
    public DbSet<GeneratedPersonalLetter> GeneratedPersonalLetters { get; set; } = default!;


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        
        builder.Entity<GeneratedCv>()
            .HasKey(c => c.Id);

        builder.Entity<GeneratedCv>()
            .Property(c => c.UserId)
            .IsRequired();

        builder.Entity<GeneratedPersonalLetter>()
            .HasKey(c => c.Id);

        builder.Entity<GeneratedPersonalLetter>()
            .Property(c => c.UserId)
            .IsRequired();
    }
}
