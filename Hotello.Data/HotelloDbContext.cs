using Hotello.Data.Configs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hotello.Data;

public class HotelloDbContext : IdentityDbContext<ApiUser>
{
    public HotelloDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Country> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RoleConfig());
        modelBuilder.ApplyConfiguration(new CountryConfig());
        modelBuilder.ApplyConfiguration(new HotelConfig());
    }
}
