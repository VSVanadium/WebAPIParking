
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using WebAPIParking.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace WebAPIParking.DataRepositories
{
    public class ConfigDatabaseContext : DbContext
{
        private readonly Configuration _configuration;
        public ConfigDatabaseContext(DbContextOptions<ConfigDatabaseContext> options, Configuration configuration)
        : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<ParkingModel> ParkedVehicles { get; set; }
        public DbSet<FloorModel> Floors { get; set; }
        public DbSet<SlotModel> Slots { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var appSettings = _configuration.AppSettings?.Settings;
            var Connectionstring = (appSettings?["databaseConnectionString"]?.Value ?? "");
            
            optionsBuilder.UseSqlServer(Connectionstring);
           
            optionsBuilder.EnableDetailedErrors();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParkingModel>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
