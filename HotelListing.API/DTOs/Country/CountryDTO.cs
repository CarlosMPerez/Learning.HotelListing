namespace HotelListing.API.DTOs;

public class CountryDTO : BaseCountryDTO
{
    public int Id { get; set; }

    public List<HotelDTO> Hotels { get; set; }
}
