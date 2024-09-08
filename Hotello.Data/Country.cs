using System.ComponentModel.DataAnnotations;

namespace Hotello.Data;

public class Country
{
    public int Id { get; set; }
    [Required]
    public required string Name { get; set; }
    public string? ShortName { get; set; }
    public virtual IList<Hotel>? Hotels { get; set; }
}