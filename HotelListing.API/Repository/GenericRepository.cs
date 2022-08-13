using HotelListing.API.Contracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly HotelListingDbContext ctx;

    public GenericRepository(HotelListingDbContext context)
    {
        this.ctx = context;
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
