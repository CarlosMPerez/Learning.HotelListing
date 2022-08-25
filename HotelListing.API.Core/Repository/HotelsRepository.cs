using AutoMapper;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Core.Repository;

public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
{
    private readonly HotelListingDbContext ctx;
    private readonly IMapper mapper;

	public HotelsRepository(HotelListingDbContext context, IMapper mapper) : base(context, mapper)
	{
		this.ctx = context;
        this.mapper = mapper;
	}

    public async Task<Hotel> GetDetails(int id)
    {
        return await ctx.Hotels
            .Include(x => x.Country)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
