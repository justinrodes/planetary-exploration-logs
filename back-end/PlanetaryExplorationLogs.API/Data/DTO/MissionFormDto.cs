using System.ComponentModel.DataAnnotations;

namespace PlanetaryExplorationLogs.API.Data.DTO
{
  public class MissionFormDto
  {
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";

    [Required]
    public DateTime Date { get; set; } = DateTime.UtcNow;

    [Required]
    [Range(1, int.MaxValue)]
    public int PlanetId { get; set; }

    [Required]
    [StringLength(500)]
    public string Description { get; set; } = "";
  }
}
