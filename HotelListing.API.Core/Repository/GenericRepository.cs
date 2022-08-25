using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Core.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly HotelListingDbContext ctx;
    private readonly IMapper mapper;

    public GenericRepository(HotelListingDbContext context, IMapper mapper)
    {
        this.ctx = context;
        this.mapper = mapper;
    }

    public async Task<T> AddAsync(T entity)
    {
        await ctx.AddAsync(entity);
        await ctx.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        ctx.Set<T>().Remove(entity);
        await ctx.SaveChangesAsync();
    }

    public async Task<bool> Exists(int id)
    {
        var entity = await GetAsync(id);
        return entity != null;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await ctx.Set<T>().ToListAsync();
    }

    public async Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters)
    {
        var totalSize = await ctx.Set<T>().CountAsync();
        var items = await ctx.Set<T>()
            .Skip(queryParameters.StartIndex) // Where to start
            .Take(queryParameters.PageSize) // How many to take
            .ProjectTo<TResult>(mapper.ConfigurationProvider) // (*)
            .ToListAsync();

        // (*) Which columns to extract according to the mapper cfg.
        // This line eliminates the need of mapping in the controller and converts from T to TResult

        return new PagedResult<TResult>
        {
            Items = items,
            PageNumber = queryParameters.PageNumber,
            RecordNumber = queryParameters.PageSize,
            TotalCount = totalSize
        };
    }

    public async Task<T> GetAsync(int? id)
    {
        if (id == null) return null;

        return await ctx.Set<T>().FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        ctx.Update(entity);
        await ctx.SaveChangesAsync();
    }
}
