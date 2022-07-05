using InternTask1.Models.Data;
using InternTask1.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternTask1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDataManager dataManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDataManager dataManager)
        {
            _logger = logger;
            this.dataManager = dataManager;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public ActionResult<List<WeatherForecast>> Get()
        {
            return dataManager.GetForecasts();
        }
        [HttpPost]
        public void Post([FromBody] WeatherForecast createSample)
        {
            dataManager.SaveWeather(createSample);
        }
        [HttpGet("{City}/{Date}")]
        public ActionResult<List<WeatherForecast>> Get(string city, string date)
        {
            return dataManager.GetForecasts(city, date);
        }

    }
}