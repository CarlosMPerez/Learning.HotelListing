using AutoMapper;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Core.Repository;

public class CountriesRepository : GenericRepository<Country>,  ICountriesRepository
{
	private readonly HotelListingDbContext ctx;
	public CountriesRepository(HotelListingDbContext context, IMapper mapper) : base(context, mapper)
	{
		this.ctx = context;
	}

	public async Task<Country> GetDetails(int id)
	{
		return await ctx.Countries
			.Include(x => x.Hotels)
			.FirstOrDefaultAsync(x => x.Id == id);
	}
}
