using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Core.Exceptions;
using HotelListing.API.Core.Middleware;
using HotelListing.API.Data;
using HotelListing.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Core.Repository;

/// <summary>
/// Generic Repository
/// Does the mapping from DTOs and db models in here using TResult and TSources
/// Old version (which forced to do in the mapping in the controller) is commented
/// TResult and TSource 
/// </summary>
/// <typeparam name="T">The DATA model</typeparam>
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly HotelListingDbContext ctx;
    private readonly IMapper mapper;

    /// <summary>
    /// Ctor injecting the DBContext and the mapper
    /// </summary>
    /// <param name="context">DBContext</param>
    /// <param name="mapper">Mapper</param>
    public GenericRepository(HotelListingDbContext context, IMapper mapper)
    {
        this.ctx = context;
        this.mapper = mapper;
    }

    /// <summary>
    /// Gets all entities from the database
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <returns>A list of entities</returns>
    public async Task<List<TResult>> GetAllAsync<TResult>()
    {
        return await ctx.Set<T>() // From a set of T
            .ProjectTo<TResult>(mapper.ConfigurationProvider) // Project the mapping to TResult
            .ToListAsync(); // And convert to list
    }

    /// <summary>
    /// Gets all using query parameters, allowing pagination
    /// </summary>
    /// <typeparam name="TResult">The generic DTO type to return</typeparam>
    /// <param name="queryParameters">The query parameters</param>
    /// <returns>A PagedResult object, containing the items and pagination information</returns>
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
            Items = items, // The returning items (can be null)
            PageNumber = (int)Math.Ceiling((decimal)(queryParameters.StartIndex / queryParameters.PageSize)) + 1, // The current page number is calculated
            RecordNumber = queryParameters.PageSize, // Total number of records per page
            TotalCount = totalSize // Total number of records available
        };
    }

    /// <summary>
    /// We don't expose GetAsync<typeparamref name="T"/>
    /// Only used inside this class to retrieve a single entity and raise a NotFoundException 
    /// if it's not found
    /// It's the same as GetAsync of TResult but WITHOUT mapping
    /// </summary>
    /// <param name="id">the id of the entity</param>
    /// <returns>The entity</returns>
    /// <exception cref="NotFoundException">Personalized NotFoundException if the id is wrong</exception>
    private async Task<T> GetAsync(int? id)
    {
        var result = await ctx.Set<T>().FindAsync(id);
        if (result is null) throw new NotFoundException(typeof(T).Name, id.HasValue ? id : "No Key provided");
        return result;
    }

    /// <summary>
    /// Gets a single entity by its id
    /// </summary>
    /// <typeparam name="TResult">The entity DTO type</typeparam>
    /// <param name="id">The unique id of the entity</param>
    /// <returns>The DTO of the entity</returns>
    /// <exception cref="NotFoundException">Personalized NotFoundException if there's no entity with the id provided</exception>
    public async Task<TResult> GetAsync<TResult>(int? id)
    {
        var result = await GetAsync(id);
        return mapper.Map<TResult>(result);
    }

    /// <summary>
    /// Adds a new entity
    /// </summary>
    /// <typeparam name="TSource">The DTO</typeparam>
    /// <typeparam name="TResult">The new entity</typeparam>
    /// <param name="source">The DTO to insert into the database</param>
    /// <returns>A new entity inserted into the database</returns>
    public async Task<TResult> AddAsync<TSource, TResult>(TSource source)
    {
        var entity = mapper.Map<T>(source);

        await ctx.AddAsync(entity);
        await ctx.SaveChangesAsync();

        return mapper.Map<TResult>(entity);
    }

    /// <summary>
    /// Updates an entity
    /// </summary>
    /// <typeparam name="TSource">The entity db model</typeparam>
    /// <param name="id">The Id</param>
    /// <param name="source">The source object</param>
    /// <returns>The updated entity</returns>
    public async Task UpdateAsync<TSource>(int id, TSource source)
    {
        var entity = await GetAsync(id);

        mapper.Map(source, entity);
        ctx.Update(entity);
        await ctx.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes an entity from the database
    /// </summary>
    /// <param name="id">The unique id of the entity</param>
    /// <returns></returns>
    public async Task DeleteAsync(int id)
    {
        var entity = await GetAsync(id);

        ctx.Set<T>().Remove(entity);
        await ctx.SaveChangesAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> Exists(int id)
    {
        return (await GetAsync(id)) != null;
    }

    //private async Task<T> AddAsync(T entity)
    //{
    //    await ctx.AddAsync(entity);
    //    await ctx.SaveChangesAsync();
    //    return entity;
    //}

    //public async Task<List<T>> GetAllAsync()
    //{
    //    return await ctx.Set<T>().ToListAsync();
    //}

    //public async Task UpdateAsync(T entity)
    //{
    //    ctx.Update(entity);
    //    await ctx.SaveChangesAsync();
    //}
}
