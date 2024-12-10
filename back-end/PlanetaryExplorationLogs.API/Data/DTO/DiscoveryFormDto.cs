using System.ComponentModel.DataAnnotations;

namespace PlanetaryExplorationLogs.API.Data.DTO
{
  public class DiscoveryFormDto
  {
    [Required]
    [Range(1, int.MaxValue)]
    public int MissionId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int DiscoveryTypeId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(200)]
    public string? Location { get; set; }
  }
}
