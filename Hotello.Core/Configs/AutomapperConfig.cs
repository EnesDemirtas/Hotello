using AutoMapper;
using Hotello.Data;
using Hotello.Core.Models.Country;
using Hotello.Core.Models.Hotel;
using Hotello.Core.Models.User;

namespace Hotello.Core.Configs;

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
