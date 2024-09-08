using AutoMapper;
using Hotello.API.Contracts;
using Hotello.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Hotello.API.Repositories;

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
