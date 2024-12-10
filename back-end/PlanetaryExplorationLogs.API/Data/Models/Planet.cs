using System.ComponentModel.DataAnnotations;

namespace PlanetaryExplorationLogs.API.Data.Models
{
  public class Planet
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";

    [Required]
    [StringLength(50)]
    public string Type { get; set; } = ""; // e.g., Terrestrial, Gas Giant, Ice Giant, Dwarf

    [StringLength(200)]
    public string? Climate { get; set; } // e.g., Temperate, Arid, Frozen

    [StringLength(200)]
    public string? Terrain { get; set; } // e.g., Mountains, Oceans, Deserts

    [StringLength(100)]
    public string? Population { get; set; } // e.g., "Uninhabited", "Sparse Colonies"
  }
}
