using System.ComponentModel.DataAnnotations;

namespace Hotello.API.Models.Hotel;

public class HotelDTO
{
    public int Id { get; set; }
    [Required]
    public required string Name { get; set; }
    public string? Address { get; set; }
    public double? Rating { get; set; }
    public int? CountryId { get; set; }
}
