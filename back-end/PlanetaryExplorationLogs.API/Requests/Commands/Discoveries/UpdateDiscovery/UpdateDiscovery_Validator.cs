using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.Models;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Discoveries.UpdateDiscovery
{
  public class UpdateDiscovery_Validator : ValidatorBase
  {
    private readonly Discovery _discovery;

    public UpdateDiscovery_Validator(PlanetExplorationDbContext context, Discovery discovery)
      : base(context)
    {
      _discovery = discovery;
    }

    public override async Task<RequestResult> ValidateAsync()
    {
      if (_discovery.Id < 1)
      {
        return await InvalidResultAsync(
            HttpStatusCode.BadRequest,
            "The discovery must have a valid ID.");
      }

      if (_discovery.MissionId < 1)
      {
        return await InvalidResultAsync(
            HttpStatusCode.BadRequest,
            "The discovery must have a valid mission ID.");
      }

      if (_discovery.DiscoveryTypeId < 1)
      {
        return await InvalidResultAsync(
            HttpStatusCode.BadRequest,
            "The discovery must have a valid discovery type ID.");
      }

      if (string.IsNullOrEmpty(_discovery.Name.Trim()))
      {
        return await InvalidResultAsync(
            HttpStatusCode.BadRequest,
            "The discovery must have a name.");
      }

      var discoveryExists = await DbContext.Discoveries.AnyAsync(d => d.Id == _discovery.Id);

      if (!discoveryExists)
      {
        return await InvalidResultAsync(
            HttpStatusCode.NotFound,
            "No discovery exists with the given ID.");
      }

      var missionExists = await DbContext.Missions.AnyAsync(m => m.Id == _discovery.MissionId);

      if (!missionExists)
      {
        return await InvalidResultAsync(
            HttpStatusCode.BadRequest,
            "The discovery must have an existing mission ID.");
      }

      var typeExists = await DbContext.DiscoveryTypes.AnyAsync(dt => dt.Id == _discovery.DiscoveryTypeId);

      if (!typeExists)
      {
        return await InvalidResultAsync(
            HttpStatusCode.BadRequest,
            "The discovery must have an existing discovery type ID.");
      }

      return await ValidResultAsync();
    }
  }
}
