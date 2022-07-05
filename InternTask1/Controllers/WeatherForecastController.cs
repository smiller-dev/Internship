using InternTask1.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace InternTask1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IWeather _weath;
        private readonly WeatherDbContext _context;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherDbContext _weatherDbContext, IWeather weath)
        {
            _logger = logger;
            this._context = _weatherDbContext;
            this._weath = weath;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public ActionResult<List<WeatherForecast>> Get()
        {
            var item = _weath.weatherForecasts.ToList();
            return new List<WeatherForecast>(item);
        }
        [HttpPost]
        public void Post([FromBody] WeatherForecast createSample)
        {
            using(var trn = _context.Database.BeginTransaction()) //create transaction

            {

                try

                {

                    if (_context.weatherForecasts.Any(c => c.Id == createSample.Id)) //ID exists

                    {

                        var item = _context.weatherForecasts.Single(c => c.Id == createSample.Id);

                        item.TemperatureF = createSample.TemperatureF;

                        item.City = createSample.City;

                        item.Date = createSample.Date;

                        item.TemperatureC = createSample.TemperatureC;

                    }

                    else if (_context.weatherForecasts.Any(c => c.City == createSample.City && c.Date == createSample.Date)) //City & Date exists

                    {

                        var item = _context.weatherForecasts.Single(c => c.City == createSample.City && c.Date == createSample.Date);

                        item.TemperatureF = createSample.TemperatureF;

                        item.TemperatureC = createSample.TemperatureC;

                    }

                    else //no matching records

                    {

                        _weath.weatherForecasts.Add(createSample);

                    }

                    _context.SaveChanges(); //update context

                    trn.Commit(); //commit changes to database

                }

                catch

                {

                    trn.Rollback(); //rollback all changes

                    throw; //send exception to caller

                } }
            }
        [HttpGet("{City}/{Date}")]
        public ActionResult<List<WeatherForecast>> Get(string city, string date)
        {
            var itemToReturn = _weath.weatherForecasts.Where(x => x.City == city && x.Date == date).ToList();
            return new List<WeatherForecast>(itemToReturn);
        }

    }
}