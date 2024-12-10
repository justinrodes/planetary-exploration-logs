using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.Models;

namespace PlanetaryExplorationLogs.API.IntegrationTests;

public class SqliteFixture : IDisposable
{
  private DbConnection? connection;

  public void Initialize(CustomWebApplicationFactory app)
  {
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<PlanetExplorationDbContext>();
    connection = scope.ServiceProvider.GetRequiredService<DbConnection>();
    db.Database.Migrate();
    SeedData(db);
  }

  private static void SeedData(PlanetExplorationDbContext db)
  {
    if (!db.Planets.Any())
    {
      db.Planets.AddRange(
          new Planet
          {
            Id = 1,
            Name = "Terra Nova",
            Type = "Terrestrial",
            Climate = "Temperate",
            Terrain = "Mountains and Oceans",
            Population = "Sparse Colonies"
          },
          new Planet
          {
            Id = 2,
            Name = "Xenon Prime",
            Type = "Gas Giant",
            Climate = "Extreme Storms",
            Terrain = "Gaseous Layers",
            Population = "Uninhabited"
          },
          new Planet
          {
            Id = 3,
            Name = "Glaciera",
            Type = "Ice Giant",
            Climate = "Frozen",
            Terrain = "Ice Plains and Subsurface Oceans",
            Population = "Uninhabited"
          },
          new Planet
          {
            Id = 4,
            Name = "Dwarfia",
            Type = "Dwarf",
            Climate = "Arid",
            Terrain = "Deserts",
            Population = "Uninhabited"
          }
      );
    }

    if (!db.DiscoveryTypes.Any())
    {
      db.DiscoveryTypes.AddRange(
          new DiscoveryType
          {
            Id = 1,
            Name = "Geological",
            Description = "Discoveries related to the planet's geology, such as mineral deposits and tectonic activity."
          },
          new DiscoveryType
          {
            Id = 2,
            Name = "Biological",
            Description = "Discoveries pertaining to life forms, ecosystems, and biological phenomena."
          },
          new DiscoveryType
          {
            Id = 3,
            Name = "Technological",
            Description = "Findings related to advanced technologies, alien artifacts, or unexplained mechanisms."
          },
          new DiscoveryType
          {
            Id = 4,
            Name = "Atmospheric",
            Description = "Discoveries involving atmospheric compositions, weather patterns, and climate anomalies."
          }
      );
    }

    if (!db.Missions.Any())
    {
      db.Missions.Add(
        new Mission
        {
          Id = 1,
          Name = "Test Mission",
          Date = DateTime.Now,
          PlanetId = 1,
          Description = "This is only a test",
          Discoveries = [
            new Discovery {
                  Id = 1,
                  Name = "First Test Discovery",
                  DiscoveryTypeId = 1,
                  Location = "First Location",
                  Description = "First Description"
                },
                new Discovery {
                  Id = 2,
                  Name = "Second Test Discovery",
                  DiscoveryTypeId = 2,
                  Location = "Second Location",
                  Description = "Second Description"
                }
          ]
        }
      );
    }

    db.SaveChanges();
  }

  public void Dispose()
  {
    connection?.Dispose();
    GC.SuppressFinalize(this);
  }
}
