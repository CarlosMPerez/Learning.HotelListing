using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Core.DTOs;
using HotelListing.API.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OData.Query;
using HotelListing.API.Core.Middleware;

namespace HotelListing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly ICountriesRepository repo;

    /// <summary>
    /// Ctor injecting the concrete repository
    /// </summary>
    /// <param name="repository">The ICountriesRepository</param>
    public CountriesController(ICountriesRepository repository)
    {
        this.repo = repository;
    }

    /// <summary>
    /// ROUTE => GET: api/Countries/GetAll
    /// Get a list of all countries without parameters
    /// </summary>
    /// <returns>List of CountryItemDTO</returns>
    [HttpGet("GetAll")]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<CountryItemDTO>>> GetCountries()
    {
        var countries = await repo.GetAllAsync<CountryItemDTO>();
        return Ok(countries);
    }

    /// <summary>
    /// ROUTE => GET: api/Countries/?StartIndex=0&PageSize=25&PageNumber=1
    /// Get a list of all countries using parameters (for pagination)
    /// </summary>
    /// <param name="queryParams">The query parameters</param>
    /// <returns>List of CountryItemDTO</returns>
    [HttpGet]
    public async Task<ActionResult<PagedResult<CountryItemDTO>>> GetPagedCountries([FromQuery] QueryParameters queryParams)
    {
        var pagedResult = await repo.GetAllAsync<CountryItemDTO>(queryParams);
        return Ok(pagedResult);
    }

    /// <summary>
    /// ROUTE => GET: api/Countries/5
    /// Gets a single CountryDTO by its Id
    /// </summary>
    /// <param name="id">the id</param>
    /// <returns>A CountryDTO</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<CountryDTO>> GetCountry(int id)
    {
        var country = await repo.GetDetails(id);
        return Ok(country);
    }

    /// <summary>
    /// ROUTE => POST: api/Countries
    /// Inserts a new country into the database
    /// Authorization needed
    /// </summary>
    /// <param name="createDTO">The new country data</param>
    /// <returns>the created country DTO</returns>
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CountryDTO>> PostCountry([FromBody] CreateCountryDTO createDTO)
    {
        var country = await repo.AddAsync<CreateCountryDTO, CountryDTO>(createDTO);
        return CreatedAtAction(nameof(PostCountry), new { id = country.Id }, country);
    }

    /// <summary>
    /// ROUTE => PUT: api/Countries/5
    /// Updates an existing country in the database
    /// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// Authorization needed
    /// </summary>
    /// <param name="id">The id of the country</param>
    /// <param name="updateDTO">The updated country data</param>
    /// <returns>An action result</returns>
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> PutCountry(int id, UpdateCountryDTO updateDTO)
    {
        if (id != updateDTO.Id) return BadRequest("Invalid Record Id");

        try
        {
            await repo.UpdateAsync(id, updateDTO);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await repo.Exists(id)) return NotFound();
            else throw;
        }

        return NoContent();
    }

    /// <summary>
    /// ROUTE => DELETE: api/Countries/5
    /// Deletes the entity with the provided id
    /// Admin authorization needed
    /// </summary>
    /// <param name="id">the Id of the entity to delete</param>
    /// <returns>No Content</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles ="Administrator")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        await repo.DeleteAsync(id);
        return NoContent();
    }
}
