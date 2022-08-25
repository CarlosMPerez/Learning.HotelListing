using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Core.DTOs;
using HotelListing.API.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using HotelListing.API.Core.Middleware;
using Microsoft.AspNetCore.OData.Query;

namespace HotelListing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
    private readonly IHotelsRepository repo;

    /// <summary>
    /// Ctor injecting the concrete repository
    /// </summary>
    /// <param name="repository">The IHotelsRepository</param>
    public HotelsController(IHotelsRepository repository)
    {
        this.repo = repository;
    }

    /// <summary>
    /// ROUTE => GET: api/hotels/getall
    /// Get a list of all hotels without parameters
    /// </summary>
    /// <returns>List of HotelItemDTO</returns>
    [HttpGet("GetAll")]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<HotelItemDTO>>> GetHotels()
    {
        var hotels = await repo.GetAllAsync<HotelItemDTO>();
        return Ok(hotels);
    }

    /// <summary>
    /// ROUTE => GET: api/hotels/?StartIndex=0&PageSize=25&PageNumber=1
    /// Get a list of all hotels using parameters (for pagination)
    /// </summary>
    /// <param name="queryParams">The query parameters</param>
    /// <returns>List of HotelItemDTO</returns>
    [HttpGet]
    public async Task<ActionResult<PagedResult<HotelItemDTO>>> GetPagedCountries([FromQuery] QueryParameters queryParams)
    {
        var pagedResult = await repo.GetAllAsync<HotelItemDTO>(queryParams);
        return Ok(pagedResult);
    }

    /// <summary>
    /// ROUTE => GET: api/hotels/5
    /// Gets a single HotelDTO by its Id
    /// </summary>
    /// <param name="id">the id</param>
    /// <returns>A HotelDTO</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<HotelDTO>> GetHotel(int id)
    {
        var hotel = await repo.GetDetails(id);
        return Ok(hotel);
    }

    /// <summary>
    /// ROUTE => POST: api/hotels
    /// Inserts a new hotel into the database
    /// Authorization needed
    /// </summary>
    /// <param name="createDTO">The new hotel data</param>
    /// <returns>The created hotel DTO</returns>
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<HotelDTO>> PostHotel([FromBody] CreateHotelDTO createDTO)
    {
        var hotel = await repo.AddAsync<CreateHotelDTO, HotelDTO>(createDTO);
        return CreatedAtAction(nameof(PostHotel), new { id = hotel.Id }, hotel);
    }

    /// <summary>
    /// ROUTE => PUT: api/hotels/5
    /// Updates an existing hotel in the database
    /// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// Authorization needed
    /// </summary>
    /// <param name="id">The id of the hotel</param>
    /// <param name="updateDTO">The updated hotel data</param>
    /// <returns>An action result</returns>
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> PutHotel(int id, UpdateHotelDTO updateDTO)
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
    /// ROUTE => DELETE: api/hotels/5
    /// Deletes the entity with the provided id
    /// Admin authorization needed
    /// </summary>
    /// <param name="id">the Id of the entity to delete</param>
    /// <returns>No Content</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        await repo.DeleteAsync(id);
        return NoContent();
    }
}

