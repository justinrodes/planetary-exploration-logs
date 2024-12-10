using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.DTO;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Discoveries.GetDiscoveryTypes
{
  public class GetDiscoveryTypesDropdownList_Handler : HandlerBase<List<DiscoveryTypeDropdownDto>>
  {
    public GetDiscoveryTypesDropdownList_Handler(PlanetExplorationDbContext context)
        : base(context)
    {
    }

    public override async Task<RequestResult<List<DiscoveryTypeDropdownDto>>> HandleAsync()
    {
      var discoveryTypes = await DbContext.DiscoveryTypes
          .OrderBy(t => t.Name)
          .Select(t => new DiscoveryTypeDropdownDto
          {
            Id = t.Id,
            Name = t.Name
          })
          .ToListAsync();

      var result = new RequestResult<List<DiscoveryTypeDropdownDto>>
      {
        Data = discoveryTypes
      };

      return result;
    }
  }
}
