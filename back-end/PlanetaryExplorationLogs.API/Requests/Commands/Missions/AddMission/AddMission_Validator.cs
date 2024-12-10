using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.DTO;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Missions.AddMission
{
  public class AddMission_Validator : ValidatorBase
  {
    private readonly MissionFormDto _mission;

    public AddMission_Validator(PlanetExplorationDbContext context, MissionFormDto mission)
      : base(context)
    {
      _mission = mission;
    }

    public override async Task<RequestResult> ValidateAsync()
    {
      if (string.IsNullOrEmpty(_mission.Name.Trim()))
      {
        return await InvalidResultAsync(
          HttpStatusCode.BadRequest,
          "The mission must have a name.");
      }

      if (_mission.PlanetId < 1)
      {
        return await InvalidResultAsync(
            HttpStatusCode.BadRequest,
            "The mission must have a valid planet ID.");
      }

      var planetExists = await DbContext.Planets.AnyAsync(p => p.Id == _mission.PlanetId);

      if (!planetExists)
      {
        return await InvalidResultAsync(
            HttpStatusCode.BadRequest,
            "The mission must have an existing planet ID.");
      }

      return await ValidResultAsync();
    }
  }
}
