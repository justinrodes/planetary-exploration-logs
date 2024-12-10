using PlanetaryExplorationLogs.API.Data.Context;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Planets.DeletePlanet
{
  public class DeletePlanet_Command : RequestBase<int>
  {
    private readonly int _planetId;

    public DeletePlanet_Command(PlanetExplorationDbContext context, int planetId)
      : base(context)
    {
      _planetId = planetId;
    }

    public override IValidator Validator =>
      new DeletePlanet_Validator(DbContext, _planetId);

    public override IHandler<int> Handler =>
      new DeletePlanet_Handler(DbContext, _planetId);
  }
}
