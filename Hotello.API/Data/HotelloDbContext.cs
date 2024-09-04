using Microsoft.EntityFrameworkCore;

namespace Hotello.API.Data;

public class HotelloDbContext : DbContext
{
    public HotelloDbContext(DbContextOptions options) : base(options)
    {
        
    }
}
