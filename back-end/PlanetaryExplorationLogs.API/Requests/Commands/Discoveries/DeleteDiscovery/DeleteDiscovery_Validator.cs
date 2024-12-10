using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Discoveries.DeleteDiscovery
{
  public class DeleteDiscovery_Validator : ValidatorBase
  {
    private readonly int _discoveryId;

    public DeleteDiscovery_Validator(PlanetExplorationDbContext context, int discoveryId)
      : base(context)
    {
      _discoveryId = discoveryId;
    }

    public override async Task<RequestResult> ValidateAsync()
    {
      var discoveryExists = await DbContext.Discoveries.AnyAsync(d => d.Id == _discoveryId);

      if (!discoveryExists)
      {
        return await InvalidResultAsync(
          HttpStatusCode.BadRequest,
          "No discovery exists with the given ID.");
      }

      return await ValidResultAsync();
    }
  }
}
