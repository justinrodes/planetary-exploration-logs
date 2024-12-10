using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Missions.DeleteMission
{
  public class DeleteMission_Handler : HandlerBase<int>
  {
    private readonly int _missionId;

    public DeleteMission_Handler(PlanetExplorationDbContext context, int missionId)
        : base(context)
    {
      _missionId = missionId;
    }

    public override async Task<RequestResult<int>> HandleAsync()
    {
      var mission = await DbContext.Missions.FindAsync(_missionId);
      if (mission != null)
      {
        DbContext.Missions.Remove(mission);
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
