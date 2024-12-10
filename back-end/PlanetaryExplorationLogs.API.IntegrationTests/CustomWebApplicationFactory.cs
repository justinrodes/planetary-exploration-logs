using System.Data.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PlanetaryExplorationLogs.API.Data.Context;

namespace PlanetaryExplorationLogs.API.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureTestServices(services =>
    {
      services.RemoveAll(typeof(DbContextOptions<PlanetExplorationDbContext>));

      // Create open SqliteConnection so EF won't automatically close it.
      services.AddSingleton<DbConnection>(container =>
          {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            return connection;
          });

      services.AddDbContext<PlanetExplorationDbContext>((container, options) =>
          {
            var connection = container.GetRequiredService<DbConnection>();
            options.UseSqlite(connection);
          });
    });

    builder.UseEnvironment("Development");
  }
}
