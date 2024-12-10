using PlanetaryExplorationLogs.API.Data.DTO;
using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using PlanetaryExplorationLogs.API.Data.Context;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Planets.GetPlanetsDropdownList
{
  public class GetPlanetsDropdownList_Handler : HandlerBase<List<PlanetDropdownDto>>
  {

    public GetPlanetsDropdownList_Handler(PlanetExplorationDbContext context)
        : base(context)
    {
    }

    public override async Task<RequestResult<List<PlanetDropdownDto>>> HandleAsync()
    {
      var planets = await DbContext.Planets
          .OrderBy(p => p.Name)
          .Select(p => new PlanetDropdownDto
          {
            Id = p.Id,
            Name = p.Name
          })
          .ToListAsync();

      var result = new RequestResult<List<PlanetDropdownDto>> { Data = planets };

      return result;
    }
  }
}
