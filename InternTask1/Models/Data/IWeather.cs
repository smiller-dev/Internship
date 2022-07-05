using Microsoft.EntityFrameworkCore;

namespace InternTask1.Models.Data
{
    public interface IWeather
    {
        public DbSet<WeatherForecast> weatherForecasts { get; set; }
   

        }
}
