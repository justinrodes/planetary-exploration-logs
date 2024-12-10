using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Data.Models;

namespace PlanetaryExplorationLogs.API.Data.Context
{
    public class DataSeeder
    {
        private readonly PlanetExplorationDbContext _context;
        public DataSeeder(PlanetExplorationDbContext context)
        {
            _context = context;
        }

        public async Task SeedData()
        {
            if (!await _context.Planets.AnyAsync())
            {
                await _context.Planets.AddRangeAsync(
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
                await _context.SaveChangesAsync();
            }

            if(!await _context.DiscoveryTypes.AnyAsync())
            {
                await _context.DiscoveryTypes.AddRangeAsync(
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
                await _context.SaveChangesAsync();
            }
        }
    }
}
