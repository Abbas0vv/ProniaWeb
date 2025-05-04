using Microsoft.EntityFrameworkCore;
using Pronia.Database.Models;

namespace Pronia.Database;

public class ProniaDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;Port=5432;Database=Pronia;User Id=postgres;Password=admin;";
        optionsBuilder.UseNpgsql(connectionString);

        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Product> Products { get; set; }
}
