using InternTask1.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace InternTask1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
           builder.Services.AddSwaggerGen();
            builder.Services.AddMvc().AddXmlSerializerFormatters();
            var connectionString = builder.Configuration.GetConnectionString("InternConnection");
            builder.Services.AddDbContext<WeatherDbContext>(db => db.UseSqlServer(connectionString));
            builder.Services.AddScoped<IWeather, WeatherDbContext>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();

            app.MapControllers();

            app.Run();
        }

    }
}