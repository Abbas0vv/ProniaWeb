using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pronia.Database.Models;

namespace Pronia.Database;

public class ProniaDbContext : IdentityDbContext<AppUser>
{
    public ProniaDbContext(DbContextOptions<ProniaDbContext> options) : base(options) { }


    public DbSet<Product> Products { get; set; }
    public DbSet<SlideBanner> SlideBanners { get; set; }
    public DbSet<Category> Categories { get; set; }
}
