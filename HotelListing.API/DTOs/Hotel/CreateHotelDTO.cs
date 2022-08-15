using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.DTOs;

public class CreateHotelDTO : BaseHotelDTO
{
    [Required]
    public int CountryId { get; set; }
}
