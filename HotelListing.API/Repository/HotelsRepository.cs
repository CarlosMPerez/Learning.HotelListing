using HotelListing.API.Contracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository;

public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
{
    private readonly HotelListingDbContext ctx;

	public HotelsRepository(HotelListingDbContext context) : base(context)
	{
		ctx = context;
	}

    public async Task<Hotel> GetDetails(int id)
    {
        return await ctx.Hotels
            .Include(x => x.Country)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
