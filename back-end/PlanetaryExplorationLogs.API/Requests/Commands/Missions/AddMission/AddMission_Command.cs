using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.DTO;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Missions.AddMission
{
  public class AddMission_Command : RequestBase<int>
  {
    private readonly MissionFormDto _mission;

    public AddMission_Command(PlanetExplorationDbContext context, MissionFormDto mission)
      : base(context)
    {
      _mission = mission;
    }

    public override IValidator Validator =>
      new AddMission_Validator(DbContext, _mission);

    public override IHandler<int> Handler =>
      new AddMission_Handler(DbContext, _mission);
  }
}
