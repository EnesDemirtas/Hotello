using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotello.Core.Contracts;
using Hotello.Core.Models;
using Hotello.Data;
using Microsoft.EntityFrameworkCore;

namespace Hotello.Core.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly HotelloDbContext _context;
    private readonly IMapper _mapper;

    public GenericRepository(HotelloDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<T> AddAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Exists(int id)
    {
        var entity = await GetAsync(id);
        return entity != null;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParams queryParams)
    {
        var totalCount = await _context.Set<T>().CountAsync();
        var items = await _context.Set<T>()
            .Skip(queryParams.StartIndex)
            .Take(queryParams.PageSize)
            .ProjectTo<TResult>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return new PagedResult<TResult>
        {
            Items = items,
            PageNumber = queryParams.PageNumber,
            RecordNumber = queryParams.PageSize,
            TotalCount = totalCount
        };
    }

    public async Task<T> GetAsync(int? id)
    {
        if (id == null) return null;

        return await _context.Set<T>().FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }
}
