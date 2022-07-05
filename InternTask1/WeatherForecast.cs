using System.ComponentModel.DataAnnotations;

namespace InternTask1
{
    public class WeatherForecast
    {[Key]
        public int Id { get; set; }
        public string Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF { get; set; }
        public string City{ get; set; }
    }
}