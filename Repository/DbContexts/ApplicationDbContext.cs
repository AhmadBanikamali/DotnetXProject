using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository.DbContexts;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Product>();
        builder.Entity<Basket>();
        
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = "Administrator",
            NormalizedName = "ADMINISTRATOR",
        });

        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = "NormalUser",
            NormalizedName = "NORMALUSER",
        });

        builder.Entity<Token>()
            .Property(x => x.ExpireAt)
            .HasConversion(v => v,
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        base.OnModelCreating(builder);
    }
}