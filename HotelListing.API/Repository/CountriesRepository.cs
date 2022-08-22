using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository;

public class CountriesRepository : GenericRepository<Country>,  ICountriesRepository
{
	private readonly HotelListingDbContext ctx;
	public CountriesRepository(HotelListingDbContext context) : base(context)
	{
		ctx = context;
	}

	public async Task<Country> GetDetails(int id)
	{
		return await ctx.Countries
			.Include(x => x.Hotels)
			.FirstOrDefaultAsync(x => x.Id == id);
	}
}
