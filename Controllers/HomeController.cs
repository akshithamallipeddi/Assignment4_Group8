using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assignment4_Group8.Models;
using Assignment4_Group8.APIHandlerManager;
using Newtonsoft.Json;

namespace Assignment4_Group8.Controllers
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

        public IActionResult Incidents()
        {
            APIHandler webHandler = new APIHandler();
            List<RootData> data = webHandler.GetData();

            return View();
        }

        public IActionResult AboutUs()
        {
            ViewData["Message"] = "About Us";
            return View();
        }

        public ActionResult Chart()
        {
            List<Chart> dataPoints = new List<Chart>();

            dataPoints.Add(new Chart("THEFT/LARCENY", 30));
            dataPoints.Add(new Chart("VEHICLE BREAK-IN/THEFT", 37));
            dataPoints.Add(new Chart("BURGLARY", 14));
            dataPoints.Add(new Chart("MOTOR VEHICLE THEFT", 12));
            dataPoints.Add(new Chart("VANDALISM", 7));

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
