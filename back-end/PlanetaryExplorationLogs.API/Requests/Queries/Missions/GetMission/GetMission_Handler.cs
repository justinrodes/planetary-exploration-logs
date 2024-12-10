using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using PlanetaryExplorationLogs.API.Data.Context;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;
using PlanetaryExplorationLogs.API.Data.Models;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Missions.GetMission
{
  public class GetMission_Handler : HandlerBase<Mission>
  {
    public GetMission_Handler(PlanetExplorationDbContext context, int missionId)
      : base(context)
    {
      MissionId = missionId;
    }

    int MissionId { get; set; }

    public override async Task<RequestResult<Mission>> HandleAsync()
    {
      var mission = await DbContext.Missions
                                   .Where(m => m.Id == MissionId)
                                   .SingleAsync();

      var result = new RequestResult<Mission> { Data = mission };

      return result;
    }
  }
}
