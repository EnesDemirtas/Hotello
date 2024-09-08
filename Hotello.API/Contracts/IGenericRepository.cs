using Hotello.API.Models;

namespace Hotello.API.Contracts;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetAsync(int? id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParams queryParams);
    Task<T> AddAsync(T entity);
    Task DeleteAsync(int id);
    Task UpdateAsync(T entity);
    Task<bool> Exists(int id);
}