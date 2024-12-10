using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.Models;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Planets.GetPlanets
{
  public class GetPlanets_Query : RequestBase<List<Planet>>
  {
    public GetPlanets_Query(PlanetExplorationDbContext context)
        : base(context)
    {
    }

    public override IValidator Validator => new GetPlanets_Validator(DbContext);
    public override IHandler<List<Planet>> Handler => new GetPlanets_Handler(DbContext);
  }
}
