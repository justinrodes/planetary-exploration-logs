using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Data.Models;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using PlanetaryExplorationLogs.API.Data.Context;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Discoveries.GetDiscovery
{
  public class GetDiscovery_Handler : HandlerBase<Discovery>
  {
    public GetDiscovery_Handler(PlanetExplorationDbContext context, int discoveryId)
      : base(context)
    {
      DiscoveryId = discoveryId;
    }

    int DiscoveryId { get; set; }

    public override async Task<RequestResult<Discovery>> HandleAsync()
    {
      var discovery = await DbContext.Discoveries
                                     .Include(d => d.Mission)
                                     .Include(d => d.DiscoveryType)
                                     .Where(d => d.Id == DiscoveryId)
                                     .SingleAsync();

      var result = new RequestResult<Discovery> { Data = discovery };

      return result;
    }
  }
}
