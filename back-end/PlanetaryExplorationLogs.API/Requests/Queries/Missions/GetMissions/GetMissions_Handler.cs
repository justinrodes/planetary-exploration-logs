using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using PlanetaryExplorationLogs.API.Data.Context;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;
using PlanetaryExplorationLogs.API.Data.Models;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Missions.GetMissions
{
  public class GetMissions_Handler : HandlerBase<List<Mission>>
  {
    public GetMissions_Handler(PlanetExplorationDbContext context)
      : base(context)
    {
    }

    public override async Task<RequestResult<List<Mission>>> HandleAsync()
    {
      var missions = await DbContext.Missions
                                    .Include(m => m.Planet)
                                    .OrderByDescending(t => t.Date)
                                    .ToListAsync();

      var result = new RequestResult<List<Mission>> { Data = missions };

      return result;
    }
  }
}
