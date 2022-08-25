using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Core.DTOs;

public abstract class BaseCountryDTO
{
    [Required]
    public string Name { get; set; }
    public string Alpha2 { get; set; }

    public string Alpha3 { get; set; }

    public string NumericCode { get; set; }

}
