using Hotello.Core.Models.Hotel;

namespace Hotello.Core.Models.Country;

public class CountryDTO : BaseCountryDTO
{
    public int Id { get; set; }
    public List<HotelDTO> Hotels { get; set; }
}