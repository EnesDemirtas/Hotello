using System.ComponentModel.DataAnnotations;

namespace Hotello.Core.Models.Hotel;

public abstract class BaseHotelDTO
{
    [Required]
    public required string Name { get; set; }
    public string? Address { get; set; }
    public double? Rating { get; set; }
    public int? CountryId { get; set; }
}