using PlanetaryExplorationLogs.API.Data.Context;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Missions.DeleteMission
{
  public class DeleteMission_Command : RequestBase<int>
  {
    private readonly int _missionId;

    public DeleteMission_Command(PlanetExplorationDbContext context, int missionId)
      : base(context)
    {
      _missionId = missionId;
    }

    public override IValidator Validator =>
      new DeleteMission_Validator(DbContext, _missionId);

    public override IHandler<int> Handler =>
      new DeleteMission_Handler(DbContext, _missionId);
  }
}
