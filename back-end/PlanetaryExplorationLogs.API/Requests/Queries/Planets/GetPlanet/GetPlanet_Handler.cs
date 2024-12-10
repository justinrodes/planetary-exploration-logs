using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using PlanetaryExplorationLogs.API.Data.Context;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;
using PlanetaryExplorationLogs.API.Data.Models;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Planets.GetPlanet
{
  public class GetPlanet_Handler : HandlerBase<Planet>
  {
    public GetPlanet_Handler(PlanetExplorationDbContext context, int planetId)
      : base(context)
    {
      PlanetId = planetId;
    }

    int PlanetId { get; set; }

    public override async Task<RequestResult<Planet>> HandleAsync()
    {
      var planet = await DbContext.Planets
                                   .Where(p => p.Id == PlanetId)
                                   .SingleAsync();

      var result = new RequestResult<Planet> { Data = planet };

      return result;
    }
  }
}
