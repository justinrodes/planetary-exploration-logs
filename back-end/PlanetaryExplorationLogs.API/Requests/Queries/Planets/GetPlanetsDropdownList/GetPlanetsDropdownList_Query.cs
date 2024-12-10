using PlanetaryExplorationLogs.API.Data.DTO;
using PlanetaryExplorationLogs.API.Data.Context;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Planets.GetPlanetsDropdownList
{
  public class GetPlanetsDropdownList_Query : RequestBase<List<PlanetDropdownDto>>
  {
    public GetPlanetsDropdownList_Query(PlanetExplorationDbContext context)
        : base(context)
    {
    }

    public override IValidator Validator => new GetPlanetsDropdownList_Validator(DbContext);
    public override IHandler<List<PlanetDropdownDto>> Handler => new GetPlanetsDropdownList_Handler(DbContext);
  }
}
