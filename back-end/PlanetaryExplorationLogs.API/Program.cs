using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Converters;
using PlanetaryExplorationLogs.API.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(options =>
{
  options.LowercaseUrls = true;
});

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new StringJsonConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DataSeeder>();

// Setting up the database context
builder.Services.AddDbContext<PlanetExplorationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("PlanetaryExplorationLogsApiDatabase")));

// Allowing CORS for all origins, headers, and methods so your Angular app can call the API
builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(builder =>
  {
    builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
  });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  // Apply pending migrations
  var dbContext = scope.ServiceProvider.GetRequiredService<PlanetExplorationDbContext>();
  dbContext.Database.Migrate();

  // Seed the database with initial data
  var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
  await dataSeeder.SeedData();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
