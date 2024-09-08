using Hotello.Data;

namespace Hotello.Core.Contracts;

public interface ICountryRepository : IGenericRepository<Country>
{
    Task<Country> GetDetailsAsync(int id);
}
