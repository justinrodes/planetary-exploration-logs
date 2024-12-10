using Microsoft.AspNetCore.Mvc;
using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.DTO;
using PlanetaryExplorationLogs.API.Data.Models;
using PlanetaryExplorationLogs.API.Requests.Commands.Discoveries.DeleteDiscovery;
using PlanetaryExplorationLogs.API.Requests.Commands.Discoveries.UpdateDiscovery;
using PlanetaryExplorationLogs.API.Requests.Commands.Discoveries.AddDiscovery;
using PlanetaryExplorationLogs.API.Requests.Queries.Discoveries.GetDiscovery;
using PlanetaryExplorationLogs.API.Requests.Queries.Discoveries.GetDiscoveryTypes;
using PlanetaryExplorationLogs.API.Requests.Queries.Discoveries.GetDiscoveries;
using PlanetaryExplorationLogs.API.Utility.Patterns;

namespace PlanetaryExplorationLogs.API.Controllers
{
  [Route("api/mission/{missionId}/[controller]")]
  [ApiController]
  public class DiscoveryController : ControllerBase
  {
    private readonly PlanetExplorationDbContext _context;
    public DiscoveryController(PlanetExplorationDbContext context)
    {
      _context = context;
    }

    // GET: api/discovery-type/dropdown
    [Route("/api/discovery-type/dropdown")]
    [HttpGet]
    public async Task<ActionResult<RequestResult<List<DiscoveryTypeDropdownDto>>>> GetDiscoveryTypesDropdownList()
    {
      var query = new GetDiscoveryTypesDropdownList_Query(_context);
      return await query.ExecuteAsync();
    }

    // GET: api/mission/{missionId}/discovery
    [HttpGet]
    public async Task<ActionResult<RequestResult<List<Discovery>>>> GetDiscoveriesForMission(int missionId)
    {
      var query = new GetDiscoveries_Query(_context, missionId);
      return await query.ExecuteAsync();
    }

    // POST: api/mission/{missionId}/discovery
    [HttpPost]
    public async Task<ActionResult<RequestResult<int>>> CreateDiscoveryForMission(int missionId, [FromBody] DiscoveryFormDto discovery)
    {
      var cmd = new AddDiscovery_Command(_context, discovery);
      return await cmd.ExecuteAsync();
    }

    // GET: api/mission/{missionId}/discovery/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<RequestResult<Discovery>>> GetDiscoveryForMission(int missionId, int id)
    {
      var query = new GetDiscovery_Query(_context, id);
      return await query.ExecuteAsync();
    }

    // PUT: api/mission/{missionId}/discovery/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<RequestResult<int>>> UpdateDiscoveryForMission(int missionId, int id, [FromBody] Discovery discovery)
    {
      var cmd = new UpdateDiscovery_Command(_context, discovery);
      return await cmd.ExecuteAsync();
    }

    // DELETE: api/mission/{missionId}/discovery/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult<RequestResult<int>>> DeleteDiscoveryFromMission(int missionId, int id)
    {
      var cmd = new DeleteDiscovery_Command(_context, id);
      return await cmd.ExecuteAsync();
    }
  }
}
