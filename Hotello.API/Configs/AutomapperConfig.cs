using AutoMapper;
using Hotello.API.Data;
using Hotello.API.Models.Country;
using Hotello.API.Models.Hotel;
using Hotello.API.Models.User;

namespace Hotello.API.Configs;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<Country, CreateCountryDTO>().ReverseMap();
        CreateMap<Country, CountryDTO>().ReverseMap();
        CreateMap<Country, GetCountryDTO>().ReverseMap();
        CreateMap<Country, UpdateCountryDTO>().ReverseMap();

        CreateMap<Hotel, HotelDTO>().ReverseMap();
        CreateMap<Hotel, CreateHotelDTO>().ReverseMap();

        CreateMap<ApiUserDTO, ApiUser>().ReverseMap();
    }
}
