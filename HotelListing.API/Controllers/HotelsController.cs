using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.DTOs;
using AutoMapper;
using HotelListing.API.Contracts;


namespace HotelListing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
    private readonly IMapper mppr;
    private readonly IHotelsRepository repo;

    public HotelsController(IMapper mapper, IHotelsRepository repository)
    {
        this.mppr = mapper;
        this.repo = repository;
    }

    // GET: api/Hotels
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels()
    {
        var hotels = await repo.GetAllAsync();
        return Ok(mppr.Map<List<HotelDTO>>(hotels));
    }

    // GET: api/Hotels/5
    [HttpGet("{id}")]
    public async Task<ActionResult<HotelDTO>> GetHotel(int id)
    {
        var hotel =  await repo.GetDetails(id);
        if (hotel == null) return NotFound();
        return Ok(mppr.Map<HotelDTO>(hotel));
    }

    // PUT: api/Hotels/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHotel(int id, HotelDTO updateDTO)
    {
        if (id != updateDTO.Id) return BadRequest("Invalid Record Id");

        var hotel = await repo.GetAsync(id); // EF6 marks this file as watchable
        if (hotel == null) return NotFound();

        mppr.Map(updateDTO, hotel); // Automatically copies files from 1st into 2nd

        try
        {
            await repo.UpdateAsync(hotel);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await repo.Exists(id)) return NotFound();
            else throw;
        }

        return NoContent();
    }

    // POST: api/Hotels
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDTO createDTO)
    {
        var hotel = mppr.Map<Hotel>(createDTO);

        await repo.AddAsync(hotel);

        return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
    }

    // DELETE: api/Hotels/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        if (!await repo.Exists(id)) return NotFound();

        await repo.DeleteAsync(id);
        return NoContent();
    }
}
