using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Planets.DeletePlanet
{
  public class DeletePlanet_Validator : ValidatorBase
  {
    private readonly int _planetId;

    public DeletePlanet_Validator(PlanetExplorationDbContext context, int planetId)
      : base(context)
    {
      _planetId = planetId;
    }

    public override async Task<RequestResult> ValidateAsync()
    {
      if (!await DbContext.Planets.AnyAsync(p => p.Id == _planetId))
      {
        return await InvalidResultAsync(
          HttpStatusCode.BadRequest,
          "No planet exists with the given ID.");
      }

      return await ValidResultAsync();
    }
  }
}
