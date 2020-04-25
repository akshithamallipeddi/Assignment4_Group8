using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assignment4_Group8.Models;
using Assignment4_Group8.APIHandlerManager;
using Newtonsoft.Json;
using Assignment4_Group8.DataAccess;
using AutoMapper;
using Assignment4_Group8.Models.ViewModel;

namespace Assignment4_Group8.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ApplicationDbContext dbContext;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            dbContext = context;
            _mapper = mapper;

            APIHandler webHandler = new APIHandler();
            var dataList = webHandler.GetData();

            LoadData(dataList);
        }

        private void LoadData(List<RootData> results)
        {
            foreach (RootData a in results)
            {
                var validIds = dbContext.data.Select(t => t.objectid).ToList();
                if (!validIds.Contains(a.objectid))
                    dbContext.data.Add(a);
            }

            dbContext.SaveChanges();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Incidents()
        {
            return View(dbContext.data.ToList());
        }

        public IActionResult IncidentCounts()
        {
            var type_counts = dbContext.data.GroupBy(a => a.type).OrderBy(group => group.Key).Select(group => Tuple.Create(group.Key, group.Count())).ToList();

            List<CountbyType> counttypes = new List<CountbyType>();
            foreach (var a in type_counts)
            {
                counttypes.Add(new CountbyType { type = a.Item1, count = a.Item2 });
            }

            return View(counttypes);
        }

        [HttpGet("{i}")]
        public ActionResult Incidents(string rtype)
        {
            if (rtype == null)
            {
               // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<RootData> details = dbContext.data.Where(a => a.type == rtype).ToList();
          
            return View(details);
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
