using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.DTO;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Discoveries.GetDiscoveryTypes
{
  public class GetDiscoveryTypesDropdownList_Query : RequestBase<List<DiscoveryTypeDropdownDto>>
  {
    public GetDiscoveryTypesDropdownList_Query(PlanetExplorationDbContext context)
        : base(context)
    {
    }

    public override IHandler<List<DiscoveryTypeDropdownDto>> Handler =>
      new GetDiscoveryTypesDropdownList_Handler(DbContext);
  }
}
