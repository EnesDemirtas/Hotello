using Microsoft.EntityFrameworkCore;

namespace Hotello.API.Data;

public class HotelloDbContext : DbContext
{
    public HotelloDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Country> Countries { get; set; }
}
