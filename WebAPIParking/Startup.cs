using Microsoft.AspNetCore.Builder;
using System.Configuration;
using WebAPIParking.Controllers;
using WebAPIParking.DataRepositories;
using WebAPIParking.Models;

namespace WebAPIParking
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(
           System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(new() { ExeConfigFilename = "web.config" },
               ConfigurationUserLevel.None));

            services.AddDbContext<ConfigDatabaseContext>();

            services.AddScoped<ParkingController>();
            services.AddScoped<FloorController>();
            services.AddScoped<HomeController>();
            services.AddControllers();


            services.AddTransient<IGenericRepository<ParkingModel>, GenericRepository<ParkingModel>>();
            services.AddScoped<ParkingRepository>();
            services.AddScoped<FloorRepository>();
            services.AddScoped<SlotRepository>();

            services.AddCors(x => x.AddPolicy("all",
            builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .SetIsOriginAllowed(_ => true)));

            services.AddMvc().AddControllersAsServices();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app)
        {
            
            app.UseSwagger();
            app.UseSwaggerUI();
            
            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseCors("all");
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
