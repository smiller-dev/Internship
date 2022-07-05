using InternTask1.Models.Data;
using InternTask1.Services;
using Microsoft.EntityFrameworkCore;

namespace InternTask1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            
            builder.Services.AddSwaggerGen();
            builder.Services.AddMvc().AddXmlSerializerFormatters();
            var connectionString = builder.Configuration.GetConnectionString("InternConnection");
            builder.Services.AddDbContext<WeatherDbContext>(db => db.UseSqlServer(connectionString));
            builder.Services.AddScoped<IWeather, WeatherDbContext>();
            builder.Services.AddTransient<IDataManager, DataManager>();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBMAY9C3t2VVhiQlFadVlJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRdkJjXH9fcnFUQGBeVEE=");

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
            
            app.Run();
        }

    }
}