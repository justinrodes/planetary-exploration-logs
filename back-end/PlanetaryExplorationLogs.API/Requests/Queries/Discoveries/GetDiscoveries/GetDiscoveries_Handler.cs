using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.Models;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Discoveries.GetDiscoveries
{
  public class GetDiscoveries_Handler : HandlerBase<List<Discovery>>
  {
    public GetDiscoveries_Handler(PlanetExplorationDbContext context, int missionId)
      : base(context)
    {
      MissionId = missionId;
    }

    int MissionId { get; set; }

    public override async Task<RequestResult<List<Discovery>>> HandleAsync()
    {
      var discoveries = await DbContext.Discoveries
          .Include(d => d.DiscoveryType)
          .Where(d => d.MissionId == MissionId)
          .OrderBy(d => d.Id)
          .ToListAsync();

      var result = new RequestResult<List<Discovery>> { Data = discoveries };

      return result;
    }
  }
}
