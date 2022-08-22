using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.API.Data.Models;

public class Hotel
{
    public int Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; }
    [StringLength(500)]
    public string Address { get; set; }

    public double Rating { get; set; }

    [ForeignKey(nameof(CountryId))]
    public int CountryId { get; set; }
    
    public Country Country { get; set; }
}
