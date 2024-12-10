using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.DTO;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Planets.AddPlanet
{
  public class AddPlanet_Command : RequestBase<int>
  {
    private readonly PlanetFormDto _planet;

    public AddPlanet_Command(PlanetExplorationDbContext context, PlanetFormDto planet)
      : base(context)
    {
      _planet = planet;
    }

    public override IValidator Validator =>
      new AddPlanet_Validator(DbContext, _planet);

    public override IHandler<int> Handler =>
      new AddPlanet_Handler(DbContext, _planet);
  }
}
