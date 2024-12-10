using FluentAssertions;
using PlanetaryExplorationLogs.API.Data.Models;
using PlanetaryExplorationLogs.API.Utility.Patterns;

namespace PlanetaryExplorationLogs.API.IntegrationTests;

public class DiscoveryControllerTests(SqliteFixture fixture) : IClassFixture<SqliteFixture>
{
  private readonly SqliteFixture _fixture = fixture;

  [Fact]
  public async Task GetDiscoveriesForMission_ReturnsEmptyWithInvalidMissionId()
  {
    var app = new CustomWebApplicationFactory();
    _fixture.Initialize(app);
    var client = app.CreateClient();

    var response = await client.GetAsync("/api/mission/0/discovery");

    response.EnsureSuccessStatusCode();
    var result = await response.Content.ReadFromJsonAsync<RequestResult<List<Discovery>>>();
    result!.Data.Should().BeEmpty();
  }

  [Fact]
  public async Task GetDiscoveriesForMission_ReturnsDataWithValidMissionId()
  {
    var app = new CustomWebApplicationFactory();
    _fixture.Initialize(app);
    var client = app.CreateClient();

    var response = await client.GetAsync("/api/mission/1/discovery");

    response.EnsureSuccessStatusCode();
    var result = await response.Content.ReadFromJsonAsync<RequestResult<List<Discovery>>>();
    result!.Data.Should().NotBeEmpty();
  }

  [Fact]
  public async Task GetDiscoveriesForMission_IncludesDiscoveryType()
  {
    var app = new CustomWebApplicationFactory();
    _fixture.Initialize(app);
    var client = app.CreateClient();

    var result = await client.GetFromJsonAsync<RequestResult<List<Discovery>>>("/api/mission/1/discovery");

    result!.Data![0].DiscoveryType.Should().NotBeNull();
  }
}
