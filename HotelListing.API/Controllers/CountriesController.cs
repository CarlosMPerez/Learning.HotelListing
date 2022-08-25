using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data.Models;
using HotelListing.API.Core.DTOs;
using AutoMapper;
using HotelListing.API.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using HotelListing.API.Core.Exceptions;
using Microsoft.AspNetCore.OData.Query;

namespace HotelListing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly IMapper mppr;
    private readonly ICountriesRepository repo;

    public CountriesController(IMapper mapper, ICountriesRepository repository)
    {
        this.mppr = mapper;
        this.repo = repository;
    }

    // GET: api/Countries/GetAll
    [HttpGet("GetAll")]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<CountryItemDTO>>> GetCountries()
    {
        var countries = await repo.GetAllAsync();
        return Ok(mppr.Map<List<CountryItemDTO>>(countries));
    }

    // GET: api/Countries/?StartIndex=0&PageSize=25&PageNumber=1
    [HttpGet]
    public async Task<ActionResult<PagedResult<CountryItemDTO>>> GetPagedCountries([FromQuery] QueryParameters queryParams)
    {
        var pagedCountriesResult = await repo.GetAllAsync<CountryItemDTO>(queryParams);
        return Ok(pagedCountriesResult);
    }

    // GET: api/Countries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CountryDTO>> GetCountry(int id)
    {
        var country = await repo.GetDetails(id);
        if (country == null) throw new NotFoundException(nameof(GetCountry), id);  //return NotFound();
        return Ok(mppr.Map<CountryDTO>(country));
    }

    // PUT: api/Countries/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> PutCountry(int id, UpdateCountryDTO updateDTO)
    {
        if (id != updateDTO.Id) return BadRequest("Invalid Record Id");

        var country = await repo.GetAsync(id); // EF6 marks this file as watchable
        if (country == null) throw new NotFoundException(nameof(GetCountry), id);  //return NotFound();

        mppr.Map(updateDTO, country); // Automatically copies files from 1st into 2nd

        try
        {
            await repo.UpdateAsync(country);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await repo.Exists(id)) return NotFound();
            else throw;
        }

        return NoContent();
    }

    // POST: api/Countries
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Country>> PostCountry(CreateCountryDTO createDTO)
    {
        var country = mppr.Map<Country>(createDTO);

        await repo.AddAsync(country);        

        return CreatedAtAction("GetCountry", new { id = country.Id }, country);
    }

    // DELETE: api/Countries/5
    [HttpDelete("{id}")]
    [Authorize(Roles ="Administrator")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        if (!await repo.Exists(id)) throw new NotFoundException(nameof(GetCountry), id);  //return NotFound();

        await repo.DeleteAsync(id);
        return NoContent();
    }
}
