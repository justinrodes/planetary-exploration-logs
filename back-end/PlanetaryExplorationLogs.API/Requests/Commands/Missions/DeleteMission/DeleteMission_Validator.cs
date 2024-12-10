using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Missions.DeleteMission
{
  public class DeleteMission_Validator : ValidatorBase
  {
    private readonly int _missionId;

    public DeleteMission_Validator(PlanetExplorationDbContext context, int missionId)
      : base(context)
    {
      _missionId = missionId;
    }

    public override async Task<RequestResult> ValidateAsync()
    {
      var missionExists = await DbContext.Missions.AnyAsync(m => m.Id == _missionId);

      if (!missionExists)
      {
        return await InvalidResultAsync(
          HttpStatusCode.BadRequest,
          "No mission exists with the given ID.");
      }

      return await ValidResultAsync();
    }
  }
}
