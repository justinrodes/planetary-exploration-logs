using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.Models;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Planets.UpdatePlanet
{
  public class UpdatePlanet_Command : RequestBase<int>
  {
    private readonly Planet _planet;

    public UpdatePlanet_Command(PlanetExplorationDbContext context, Planet planet)
      : base(context)
    {
      _planet = planet;
    }

    public override IValidator Validator =>
      new UpdatePlanet_Validator(DbContext, _planet);

    public override IHandler<int> Handler =>
      new UpdatePlanet_Handler(DbContext, _planet);
  }
}
