using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.Models;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Discoveries.GetDiscovery
{
  public class GetDiscovery_Query : RequestBase<Discovery>
  {
    public GetDiscovery_Query(PlanetExplorationDbContext context, int discoveryId)
      : base(context)
    {
      DiscoveryId = discoveryId;
    }

    int DiscoveryId { get; set; }

    public override IHandler<Discovery> Handler => new GetDiscovery_Handler(DbContext, DiscoveryId);
  }
}
