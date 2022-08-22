using AutoMapper;
using HotelListing.API.Data.Models;
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

        // CountryItemDTO is going to be a readonly model, no need to reverse map
        CreateMap<Country, CountryItemDTO>();
        #endregion

        #region Hotels
        CreateMap<CreateHotelDTO, Hotel>().ReverseMap();
        CreateMap<Hotel, HotelDTO>()
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));
        CreateMap<HotelDTO, Hotel>(); // Can't reverse map
        // HotelItemDTO is going to be a readonly model, no need to reverse map
        CreateMap<Hotel, HotelItemDTO>();
        #endregion

        #region API Users
        CreateMap<APIUserDTO, APIUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email.Substring(0, src.Email.IndexOf("@"))))
            .ReverseMap();
        #endregion
    }
}
