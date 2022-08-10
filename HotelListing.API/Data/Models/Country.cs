using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Data;

public class Country
{
    public int Id { get; set; }
    
    [StringLength(250)]
    public string Name { get; set; }

    [StringLength(2)]
    public string Alpha2 { get; set; }

    [StringLength(3)]
    public string Alpha3 { get; set; }

    [StringLength(4)]
    public string NumericCode { get; set;  }

    public virtual IList<Hotel> Hotels { get; set; }
}