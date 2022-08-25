using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Core.DTOs;
using HotelListing.API.Core.Exceptions;
using HotelListing.API.Data;
using HotelListing.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Core.Repository;

public class CountriesRepository : GenericRepository<Country>,  ICountriesRepository
{
	private readonly HotelListingDbContext ctx;
	private readonly IMapper mapper;
	public CountriesRepository(HotelListingDbContext context, IMapper mapper) : base(context, mapper)
	{
		this.ctx = context;
		this.mapper = mapper;
	}

	public async Task<CountryDTO> GetDetails(int id)
	{
		var country = await ctx.Countries
			.Include(x => x.Hotels)
			.ProjectTo<CountryDTO>(mapper.ConfigurationProvider)
			.FirstOrDefaultAsync(x => x.Id == id);

		if (country == null) throw new NotFoundException(nameof(GetDetails), id);
		return country;
	}
}
