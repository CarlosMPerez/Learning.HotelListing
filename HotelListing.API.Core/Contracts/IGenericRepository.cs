using HotelListing.API.Core.Middleware;
using HotelListing.API.Data.Models;

namespace HotelListing.API.Core.Contracts;

/// <summary>
/// Interface for Generic Repository
/// Lines commented are OLD version, which needed to make the mapping in the controller
/// New versions, not commented, do the mapping with generics in the generic repository
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGenericRepository<T> where T: class
{
    //Task<T> GetAsync(int? id);

    Task<TResult> GetAsync<TResult>(int? id);

    //Task<List<T>> GetAllAsync();

    Task<List<TResult>> GetAllAsync<TResult>();

    Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters);

    //Task<T> AddAsync(T entity);

    Task<TResult> AddAsync<TSource, TResult>(TSource source);

    Task DeleteAsync(int id);

    //Task UpdateAsync(T entity);

    Task UpdateAsync<TSource>(int id, TSource source);

    Task<bool> Exists(int id);
}
