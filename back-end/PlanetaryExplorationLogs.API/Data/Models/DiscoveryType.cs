using System.ComponentModel.DataAnnotations;

namespace PlanetaryExplorationLogs.API.Data.Models
{
  public class DiscoveryType
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = ""; // e.g., Geological, Biological, Technological

    [StringLength(500)]
    public string? Description { get; set; }
  }
}
