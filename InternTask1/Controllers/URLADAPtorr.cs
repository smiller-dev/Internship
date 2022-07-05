﻿using Microsoft.AspNetCore.Mvc;

namespace InternTask1.Controllers
{
    public class URLADAPtorr : Controller
    {

            public IActionResult UrlAdaptor()
            {

                return View();

            }
       
          public IActionResult UrlDatasource([FromBody] WeatherForecastController dm)
            {
                IEnumerable DataSource = Orders.GetAllRecords();
                we operation = new DataOperations();
                if (dm.Search != null && dm.Search.Count > 0)
                {
                    DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
                }
                if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
                {
                    DataSource = operation.PerformSorting(DataSource, dm.Sorted);
                }
                if (dm.Where != null && dm.Where.Count > 0) //Filtering
                {
                    DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
                }
                int count = DataSource.Cast<Orders>().Count();
                if (dm.Skip != 0)
                {
                    DataSource = operation.PerformSkip(DataSource, dm.Skip);         //Paging
                }
                if (dm.Take != 0)
                {
                    DataSource = operation.PerformTake(DataSource, dm.Take);
                }
                return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
            }

        
            return View();
        }
    }

