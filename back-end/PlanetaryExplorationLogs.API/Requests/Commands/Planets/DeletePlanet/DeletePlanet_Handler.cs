using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Planets.DeletePlanet
{
  public class DeletePlanet_Handler : HandlerBase<int>
  {
    private readonly int _planetId;

    public DeletePlanet_Handler(PlanetExplorationDbContext context, int planetId)
        : base(context)
    {
      _planetId = planetId;
    }

    public override async Task<RequestResult<int>> HandleAsync()
    {
      var planet = await DbContext.Planets.FindAsync(_planetId);
      if (planet != null)
      {
        DbContext.Planets.Remove(planet);
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
