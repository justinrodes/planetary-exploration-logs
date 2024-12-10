using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.Models;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Missions.GetMissions
{
  public class GetMissions_Query : RequestBase<List<Mission>>
  {
    public GetMissions_Query(PlanetExplorationDbContext context)
      : base(context)
    {
    }

    public override IHandler<List<Mission>> Handler => new GetMissions_Handler(DbContext);
  }
}
