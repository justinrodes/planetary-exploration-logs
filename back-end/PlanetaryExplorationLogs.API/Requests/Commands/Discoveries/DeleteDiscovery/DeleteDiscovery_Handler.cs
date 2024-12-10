using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Discoveries.DeleteDiscovery
{
  public class DeleteDiscovery_Handler : HandlerBase<int>
  {
    private readonly int _discoveryId;

    public DeleteDiscovery_Handler(PlanetExplorationDbContext context, int discoveryId)
        : base(context)
    {
      _discoveryId = discoveryId;
    }

    public override async Task<RequestResult<int>> HandleAsync()
    {
      var discovery = await DbContext.Discoveries.FindAsync(_discoveryId);
      if (discovery != null)
      {
        DbContext.Discoveries.Remove(discovery);
        await DbContext.SaveChangesAsync();
      }

      var result = new RequestResult<int>
      {
        Data = 0
      };

      return result;
    }
  }
}
