using Hotello.API.Models.Hotel;

namespace Hotello.API.Models.Country;

public class CountryDTO : BaseCountryDTO
{
    public int Id { get; set; }
    public List<HotelDTO> Hotels { get; set; }
}