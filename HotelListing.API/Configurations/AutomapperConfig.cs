using AutoMapper;
using HotelListing.API.Data;
using HotelListing.API.DTOs;

namespace HotelListing.API.Configurations;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        #region Countries
        CreateMap<CreateCountryDTO, Country>().ReverseMap();
        CreateMap<UpdateCountryDTO, Country>().ReverseMap();

        //.ForMember(dest => dest.Alpha2, opt => opt.MapFrom(src => src.ShortName.Substring(0, 2).ToUpper()))
        //.ForMember(dest => dest.Alpha3, opt => opt.MapFrom(src => src.ShortName.Substring(0, 3).ToUpper()))

        CreateMap<Country, CountryDTO>().ReverseMap();

        // GetCountryDTO is going to be a readonly model, no need to reverse map
        CreateMap<Country, CountryItemDTO>();
        #endregion

        #region Hotels
        CreateMap<Hotel, HotelDTO>().ReverseMap();
        #endregion 
    }
}
