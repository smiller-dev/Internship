using InternTask1.Models.Data;

namespace InternTask1.Services
{
    public interface IDataManager
    {
        List<WeatherForecast> GetForecasts();
        List<WeatherForecast> GetForecasts(string city, string date);
        void SaveWeather(WeatherForecast model);
    }

    public class DataManager : IDataManager
    {
        private readonly IWeather _weath;
        private readonly WeatherDbContext _context;

        public DataManager(WeatherDbContext _weatherDbContext, IWeather weath)
        {
            this._context = _weatherDbContext;
            this._weath = weath;
        }

        public void SaveWeather(WeatherForecast model)
        {
            using var trn = _context.Database.BeginTransaction(); //create transaction
            try
            {
                if (_context.weatherForecasts.Any(c => c.Id == model.Id)) //ID exists
                {
                    var item = _context.weatherForecasts.Single(c => c.Id == model.Id);
                    item.TemperatureF = model.TemperatureF;
                    item.City = model.City;
                    item.Date = model.Date;
                    item.TemperatureC = model.TemperatureC;
                }
                else if (_context.weatherForecasts.Any(c => c.City == model.City && c.Date == model.Date)) //City & Date exists
                {
                    var item = _context.weatherForecasts.Single(c => c.City == model.City && c.Date == model.Date);
                    item.TemperatureF = model.TemperatureF;
                    item.TemperatureC = model.TemperatureC;
                }
                else //no matching records
                {
                    _weath.weatherForecasts.Add(model);
                }
                _context.SaveChanges(); //update context
                trn.Commit(); //commit changes to database
            }
            catch
            {
                trn.Rollback(); //rollback all changes
                throw; //send exception to caller
            }
        }
        public List<WeatherForecast> GetForecasts()
        {
            var items = _weath.weatherForecasts.ToList();
            return items;
        }
        public List<WeatherForecast> GetForecasts(string city, string date)
        {
            var items = _weath.weatherForecasts.Where(x => x.City == city && x.Date == date).ToList();
            return items;
        }

    }
}
