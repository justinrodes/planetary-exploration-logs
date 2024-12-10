using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Data.Models;
using System.Reflection.Emit;

namespace PlanetaryExplorationLogs.API.Data.Context
{
    /*
        How to update the database:
        
        1. Open up Developer PowerShell and navigate to the 'PlanetaryExplorationLogs.API' directory
        
        2. Run the create migration command:
            dotnet ef migrations add NameOfMigration -o Migrations --context PlanetExplorationDbContext
        
        3. Run the update database command:
            dotnet ef database update --context PlanetExplorationDbContext
    */

    public class PlanetExplorationDbContext : DbContext
    {
        public PlanetExplorationDbContext(DbContextOptions<PlanetExplorationDbContext> options) : base(options)
        {
        }

        public DbSet<Planet> Planets { get; set; } = null!;
        public DbSet<Mission> Missions { get; set; } = null!;
        public DbSet<Discovery> Discoveries { get; set; } = null!;
        public DbSet<DiscoveryType> DiscoveryTypes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Mission>()
                .HasOne(m => m.Planet)
                .WithMany()
                .HasForeignKey(m => m.PlanetId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Discovery>()
                .HasOne(d => d.Mission)
                .WithMany(m => m.Discoveries)
                .HasForeignKey(d => d.MissionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Discovery>()
                .HasOne(d => d.DiscoveryType)
                .WithMany()
                .HasForeignKey(d => d.DiscoveryTypeId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}