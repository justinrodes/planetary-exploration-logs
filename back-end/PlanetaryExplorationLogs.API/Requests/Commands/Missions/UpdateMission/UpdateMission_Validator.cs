using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.Models;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Missions.UpdateMission
{
  public class UpdateMission_Validator : ValidatorBase
  {
    private readonly Mission _mission;

    public UpdateMission_Validator(PlanetExplorationDbContext context, Mission mission)
      : base(context)
    {
      _mission = mission;
    }

    public override async Task<RequestResult> ValidateAsync()
    {
      if (_mission.Id < 1)
      {
        return await InvalidResultAsync(
            HttpStatusCode.BadRequest,
            "The mission must have a valid ID.");
      }

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

      var missionExists = await DbContext.Missions.AnyAsync(m => m.Id == _mission.Id);

      if (!missionExists)
      {
        return await InvalidResultAsync(
            HttpStatusCode.NotFound,
            "No mission exists with the given ID.");
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
