using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.Models;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Planets.GetPlanet
{
  public class GetPlanet_Query : RequestBase<Planet>
  {
    public GetPlanet_Query(PlanetExplorationDbContext context, int planetId)
      : base(context)
    {
      PlanetId = planetId;
    }

    int PlanetId { get; set; }

    public override IHandler<Planet> Handler => new GetPlanet_Handler(DbContext, PlanetId);
  }
}
