using HotelListing.API.Data.Models;

namespace HotelListing.API.Core.Contracts;

public interface ICountriesRepository : IGenericRepository<Country>
{
    Task<Country> GetDetails(int id);
}
