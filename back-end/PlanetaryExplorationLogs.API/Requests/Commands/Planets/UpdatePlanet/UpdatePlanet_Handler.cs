using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.Models;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Planets.UpdatePlanet
{
  public class UpdatePlanet_Handler : HandlerBase<int>
  {
    private readonly Planet _planet;

    public UpdatePlanet_Handler(PlanetExplorationDbContext context, Planet planet)
      : base(context)
    {
      _planet = planet;
    }

    public override async Task<RequestResult<int>> HandleAsync()
    {
      var updatedPlanet = await DbContext.Planets.FindAsync(_planet.Id);
      if (updatedPlanet != null)
      {
        updatedPlanet.Name = _planet.Name;
        updatedPlanet.Type = _planet.Type;
        updatedPlanet.Population = _planet.Population;
        updatedPlanet.Climate = _planet.Climate;
        updatedPlanet.Terrain = _planet.Terrain;
        await DbContext.SaveChangesAsync();
      }

      // Return the data
      var result = new RequestResult<int>
      {
        Data = updatedPlanet?.Id ?? -1,
        StatusCode = HttpStatusCode.OK
      };

      return result;
    }
  }

}
