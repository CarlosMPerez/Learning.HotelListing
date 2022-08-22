using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.DTOs;

public class APIUserDTO : LoginUserDTO
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
