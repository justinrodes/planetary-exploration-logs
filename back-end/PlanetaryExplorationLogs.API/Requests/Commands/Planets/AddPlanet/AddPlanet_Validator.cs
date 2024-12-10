using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.DTO;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Planets
{
  public class AddPlanet_Validator : ValidatorBase
  {
    private readonly PlanetFormDto _planet;

    public AddPlanet_Validator(PlanetExplorationDbContext context, PlanetFormDto planet)
      : base(context)
    {
      _planet = planet;
    }

    public override async Task<RequestResult> ValidateAsync()
    {
      if (string.IsNullOrEmpty(_planet.Name.Trim()))
      {
        return await InvalidResultAsync(
          HttpStatusCode.BadRequest,
          "The planet must have a name.");
      }

      if (string.IsNullOrEmpty(_planet.Type.Trim()))
      {
        return await InvalidResultAsync(
            HttpStatusCode.BadRequest,
            "The planet must have a type.");
      }

      // You can also check things in the database, if needed, such as checking if a record exists
      return await ValidResultAsync();
    }
  }
}
