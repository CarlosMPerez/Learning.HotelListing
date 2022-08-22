using HotelListing.API.Data.Models;

namespace HotelListing.API.Contracts;

public interface IHotelsRepository : IGenericRepository<Hotel>
{
    Task<Hotel> GetDetails(int id);
}
