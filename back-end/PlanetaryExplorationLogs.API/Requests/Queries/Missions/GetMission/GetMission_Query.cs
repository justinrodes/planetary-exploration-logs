using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.Models;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Missions.GetMission
{
  public class GetMission_Query : RequestBase<Mission>
  {
    public GetMission_Query(PlanetExplorationDbContext context, int missionId)
      : base(context)
    {
      MissionId = missionId;
    }

    int MissionId { get; set; }

    public override IHandler<Mission> Handler => new GetMission_Handler(DbContext, MissionId);
  }
}
