using HotelListing.API.Core.DTOs;
using HotelListing.API.Data.Models;

namespace HotelListing.API.Core.Contracts;

public interface ICountriesRepository : IGenericRepository<Country>
{
    Task<CountryDTO> GetDetails(int id);
}
