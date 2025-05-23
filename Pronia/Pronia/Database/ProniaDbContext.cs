using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pronia.Database.Models;
using Pronia.Database.Models.Account;

namespace Pronia.Database;

public class ProniaDbContext : IdentityDbContext<ProniaUser>
{
    public ProniaDbContext(DbContextOptions<ProniaDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<SlideBanner> SlideBanners { get; set; }
}
        