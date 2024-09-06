using Hotello.API.Data;

namespace Hotello.API.Contracts;

public interface ICountryRepository : IGenericRepository<Country>
{
    Task<Country> GetDetailsAsync(int id);
}
