using InternTask1.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace InternTask1.Controllers
{
    [Route("Datagrid/Index")]
    public class DatagridController : Controller
    {
        private readonly IWeather _weath;
        private readonly WeatherDbContext _context;

        public DatagridController(IWeather weath, WeatherDbContext context)
        {
            this._weath = weath;
            this._context = context;
        }

        public IActionResult Index()
        {
            var order = _weath.weatherForecasts.ToList();
            ViewBag.datasource = order;
            return View();
        }
    }
}
