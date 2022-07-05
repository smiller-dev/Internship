using Microsoft.EntityFrameworkCore;

namespace InternTask1
{
    public class WeatherForecastContext :DbContext
    {
        public WeatherForecastContext(DbContextOptions<WeatherForecastContext> db):base(db) { }
        public DbSet<WeatherForecast> weatherForecasts { get; set; }


    }
}
