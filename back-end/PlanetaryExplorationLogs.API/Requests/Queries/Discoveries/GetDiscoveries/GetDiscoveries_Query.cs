using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.Models;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Discoveries.GetDiscoveries
{
  public class GetDiscoveries_Query : RequestBase<List<Discovery>>
  {
    public GetDiscoveries_Query(PlanetExplorationDbContext context, int missionId)
      : base(context)
    {
      MissionId = missionId;
    }

    int MissionId { get; set; }

    public override IHandler<List<Discovery>> Handler => new GetDiscoveries_Handler(DbContext, MissionId);
  }
}
