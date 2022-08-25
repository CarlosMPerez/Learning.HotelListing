using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Core.DTOs;
using HotelListing.API.Core.Exceptions;
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

    public async Task<HotelDTO> GetDetails(int id)
    {
        var hotel = await ctx.Hotels
            .Include(x => x.Country)
            .ProjectTo<HotelDTO>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (hotel == null) throw new NotFoundException(nameof(GetDetails), id);
        return hotel;
    }
}
