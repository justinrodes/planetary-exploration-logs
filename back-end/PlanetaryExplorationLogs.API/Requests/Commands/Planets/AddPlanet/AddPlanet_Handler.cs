using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.DTO;
using PlanetaryExplorationLogs.API.Data.Models;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Planets
{
  public class AddPlanet_Handler : HandlerBase<int>
  {
    private readonly PlanetFormDto _planet;

    public AddPlanet_Handler(PlanetExplorationDbContext context, PlanetFormDto planet)
      : base(context)
    {
      _planet = planet;
    }

    public override async Task<RequestResult<int>> HandleAsync()
    {
      var newPlanet = new Planet
      {
        Climate = _planet.Climate,
        Name = _planet.Name,
        Population = _planet.Population,
        Terrain = _planet.Terrain,
        Type = _planet.Type
      };

      await DbContext.Planets.AddAsync(newPlanet);
      await DbContext.SaveChangesAsync();

      var result = new RequestResult<int>
      {
        Data = newPlanet.Id,
        StatusCode = HttpStatusCode.Created
      };

      return result;
    }
  }
}
