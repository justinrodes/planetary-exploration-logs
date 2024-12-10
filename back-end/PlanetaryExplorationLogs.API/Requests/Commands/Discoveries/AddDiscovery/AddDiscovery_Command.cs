using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.DTO;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Discoveries.AddDiscovery
{
  public class AddDiscovery_Command : RequestBase<int>
  {
    private readonly DiscoveryFormDto _discovery;

    public AddDiscovery_Command(PlanetExplorationDbContext context, DiscoveryFormDto discovery)
      : base(context)
    {
      _discovery = discovery;
    }

    public override IValidator Validator =>
      new AddDiscovery_Validator(DbContext, _discovery);

    public override IHandler<int> Handler =>
      new AddDiscovery_Handler(DbContext, _discovery);
  }
}
