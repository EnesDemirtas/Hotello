using AutoMapper;
using Hotello.Core.Contracts;
using Hotello.Data;
using Microsoft.EntityFrameworkCore;

namespace Hotello.Core.Repositories;

public class CountryRepository : GenericRepository<Country>, ICountryRepository
{
    private readonly HotelloDbContext _context;
    public CountryRepository(HotelloDbContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
    }

    public async Task<Country> GetDetailsAsync(int id)
    {
        return await _context.Countries.Include(c => c.Hotels).FirstOrDefaultAsync(c => c.Id == id);
    }
}
