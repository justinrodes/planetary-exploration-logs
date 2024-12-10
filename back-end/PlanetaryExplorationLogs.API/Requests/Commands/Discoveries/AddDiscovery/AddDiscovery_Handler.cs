using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.DTO;
using PlanetaryExplorationLogs.API.Data.Models;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Discoveries.AddDiscovery
{
  public class AddDiscovery_Handler : HandlerBase<int>
  {
    private readonly DiscoveryFormDto _discovery;

    public AddDiscovery_Handler(PlanetExplorationDbContext context, DiscoveryFormDto discovery)
      : base(context)
    {
      _discovery = discovery;
    }

    public override async Task<RequestResult<int>> HandleAsync()
    {
      var newDiscovery = new Discovery
      {
        MissionId = _discovery.MissionId,
        DiscoveryTypeId = _discovery.DiscoveryTypeId,
        Name = _discovery.Name,
        Description = _discovery.Description,
        Location = _discovery.Location
      };

      await DbContext.Discoveries.AddAsync(newDiscovery);
      await DbContext.SaveChangesAsync();

      var result = new RequestResult<int>
      {
        Data = newDiscovery.Id,
        StatusCode = HttpStatusCode.Created
      };

      return result;
    }
  }
}
