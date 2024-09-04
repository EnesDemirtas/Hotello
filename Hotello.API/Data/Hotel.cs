using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotello.API.Data;

public class Hotel
{
    public int Id { get; set; }
    [Required]
    public required string Name { get; set; }
    public string? Address { get; set; }
    public double? Rating { get; set; }
    public int? CountryId { get; set; }
    [ForeignKey(nameof(CountryId))]
    public Country? Country { get; set; }
}
