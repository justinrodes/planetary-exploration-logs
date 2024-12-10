using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.Models;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Missions.UpdateMission
{
  public class UpdateMission_Handler : HandlerBase<int>
  {
    private readonly Mission _mission;

    public UpdateMission_Handler(PlanetExplorationDbContext context, Mission mission)
      : base(context)
    {
      _mission = mission;
    }

    public override async Task<RequestResult<int>> HandleAsync()
    {
      var updatedMission = await DbContext.Missions.FindAsync(_mission.Id);
      if (updatedMission != null)
      {
        updatedMission.Name = _mission.Name;
        updatedMission.Date = _mission.Date;
        updatedMission.PlanetId = _mission.PlanetId;
        updatedMission.Description = _mission.Description;
        await DbContext.SaveChangesAsync();
      }

      // Return the data
      var result = new RequestResult<int>
      {
        Data = updatedMission?.Id ?? -1,
        StatusCode = HttpStatusCode.OK
      };

      return result;
    }
  }
}
