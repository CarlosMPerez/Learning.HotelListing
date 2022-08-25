using HotelListing.API.Core.DTOs;
using HotelListing.API.Data.Models;

namespace HotelListing.API.Core.Contracts;

public interface IHotelsRepository : IGenericRepository<Hotel>
{
    Task<HotelDTO> GetDetails(int id);
}
