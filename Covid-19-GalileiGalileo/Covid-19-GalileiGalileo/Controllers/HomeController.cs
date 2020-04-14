using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Covid_19_GalileiGalileo.Models;
using Covid_19_GalileiGalileo.Services;
using ChartJSCore.Models;
using ChartJSCore.Helpers;
using Covid_19_GalileiGalileo.Tool;

namespace Covid_19_GalileiGalileo.Controllers
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
            RestServices.StartUpAPI();

            CovidList<CovidData> wordHistory = new CovidList<CovidData>(RestServices.GetDataHistory().ToArray());


            ViewBag.chartTotalCases = ChartTool.CreateChart(wordHistory.ListTime(),
                new List<ChartData>() { new ChartData() 
                {
                    DatasetName = "casi positivi",
                    Data = wordHistory.TotalCases()
                }});
            ViewBag.chartNewCases = ChartTool.CreateChart(wordHistory.ListTime().Skip(1).SkipLast(1).ToList(),
                new List<ChartData>() { 
                new ChartData()
                {
                    DatasetName = "nuovi casi giornalieri",
                    Data = wordHistory.NewCases(),
                    ChartPalette = ChartPalette.blue
                }, new ChartData()
                {
                    DatasetName = "diferenza di incremento",
                    Data = wordHistory.DiferenceIncrease(),
                    ChartPalette = ChartPalette.orange
                }});

            if (wordHistory != null)
                return View(wordHistory[0]);
            else
                return BadRequest();
        }

 

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult AboutUS()
        {
            return View();
        }

        public IActionResult Info()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
