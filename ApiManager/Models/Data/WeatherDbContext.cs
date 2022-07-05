using Microsoft.EntityFrameworkCore;

namespace InternTask1.Models.Data
{
    public class WeatherDbContext : DbContext, IWeather
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> db) : base(db) { }
        public DbSet<WeatherForecast> weatherForecasts { get; set; }

    }
}
