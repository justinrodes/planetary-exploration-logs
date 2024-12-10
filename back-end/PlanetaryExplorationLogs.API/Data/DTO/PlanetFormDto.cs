using System.ComponentModel.DataAnnotations;

namespace PlanetaryExplorationLogs.API.Data.DTO
{
  public class PlanetFormDto
  {
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";

    [Required]
    [StringLength(50)]
    public string Type { get; set; } = "";

    [StringLength(200)]
    public string? Climate { get; set; }

    [StringLength(200)]
    public string? Terrain { get; set; }

    [StringLength(100)]
    public string? Population { get; set; }
  }
}
