using HotelListing.API.Data.Models;

namespace HotelListing.API.Core.Contracts;

public interface IHotelsRepository : IGenericRepository<Hotel>
{
    Task<Hotel> GetDetails(int id);
}
