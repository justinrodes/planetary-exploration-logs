using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanetaryExplorationLogs.API.Data.Models
{
  public class Mission
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Name { get; set; } = "";

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public int PlanetId { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [ForeignKey("PlanetId")]
    public virtual Planet? Planet { get; set; } = null;

    public virtual List<Discovery>? Discoveries { get; set; } = [];
  }
}
