using Microsoft.AspNetCore.Mvc;
using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.DTO;
using PlanetaryExplorationLogs.API.Data.Models;
using PlanetaryExplorationLogs.API.Requests.Commands.Planets.AddPlanet;
using PlanetaryExplorationLogs.API.Requests.Commands.Planets.DeletePlanet;
using PlanetaryExplorationLogs.API.Requests.Commands.Planets.UpdatePlanet;
using PlanetaryExplorationLogs.API.Requests.Queries.Planets.GetPlanet;
using PlanetaryExplorationLogs.API.Requests.Queries.Planets.GetPlanets;
using PlanetaryExplorationLogs.API.Requests.Queries.Planets.GetPlanetsDropdownList;
using PlanetaryExplorationLogs.API.Utility.Patterns;

namespace PlanetaryExplorationLogs.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PlanetController : ControllerBase
  {
    private readonly PlanetExplorationDbContext _context;
    public PlanetController(PlanetExplorationDbContext context)
    {
      _context = context;
    }

    // GET: api/planet/dropdown
    [HttpGet("dropdown")]
    public async Task<ActionResult<RequestResult<List<PlanetDropdownDto>>>> GetPlanetsDropdownList()
    {
      var query = new GetPlanetsDropdownList_Query(_context);
      return await query.ExecuteAsync();
    }

    // GET: api/planet
    [HttpGet]
    public async Task<ActionResult<RequestResult<List<Planet>>>> GetPlanets()
    {
      var query = new GetPlanets_Query(_context);
      return await query.ExecuteAsync();
    }

    // POST: api/planet
    [HttpPost]
    public async Task<ActionResult<RequestResult<int>>> CreatePlanet([FromBody] PlanetFormDto planet)
    {
      var cmd = new AddPlanet_Command(_context, planet);
      return await cmd.ExecuteAsync();
    }

    // GET: api/planet/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<RequestResult<Planet>>> GetPlanet(int id)
    {
      var query = new GetPlanet_Query(_context, id);
      return await query.ExecuteAsync();
    }

    // PUT: api/planet/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<RequestResult<int>>> UpdatePlanet(int id, [FromBody] Planet planet)
    {
      var cmd = new UpdatePlanet_Command(_context, planet);
      return await cmd.ExecuteAsync();
    }

    // DELETE: api/planet/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult<RequestResult<int>>> DeletePlanet(int id)
    {
      var cmd = new DeletePlanet_Command(_context, id);
      return await cmd.ExecuteAsync();
    }
  }
}
