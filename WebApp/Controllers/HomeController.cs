using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Syncfusion.EJ2.Base;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<List<WeatherForecast>> ForecastApiCallAsync()
        {
            try
            {
                var httpClient = new HttpClient();
                var uri = "https://localhost:7034/WeatherForecast";
                var webResponse = await httpClient.GetAsync(uri);
                var response = await webResponse.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                var results = JsonConvert.DeserializeObject<List<WeatherForecast>>(response);
                return results;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IActionResult> UrlDatasource([FromBody] DataManagerRequest dm)
        {
            IEnumerable<WeatherForecast> data = await ForecastApiCallAsync();
            DataOperations operation = new();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                data = operation.PerformSearching(data, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                data = operation.PerformSorting(data, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                data = operation.PerformFiltering(data, dm.Where, dm.Where[0].Operator);
            }
            int count = data.Count();            
            var item = dm.RequiresCounts ? Json(new { result = data.Skip(dm.Skip).Take(dm.Take), count }) : Json(data);
            return item;
        }
    }
}