using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.Models;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Planets.GetPlanets
{
  public class GetPlanets_Handler : HandlerBase<List<Planet>>
  {

    public GetPlanets_Handler(PlanetExplorationDbContext context)
        : base(context)
    {
    }

    public override async Task<RequestResult<List<Planet>>> HandleAsync()
    {
      var planets = await DbContext.Planets
          .OrderBy(p => p.Name)
          .ToListAsync();

      var result = new RequestResult<List<Planet>> { Data = planets };

      return result;
    }
  }
}
