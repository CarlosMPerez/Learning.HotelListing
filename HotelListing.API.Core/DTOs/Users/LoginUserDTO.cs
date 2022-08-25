using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Core.DTOs;

public class LoginUserDTO
{
    [Required]
    public string UserName { get; set; }


    [Required]
    [StringLength(15, ErrorMessage = "Your password must have between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; }

}
