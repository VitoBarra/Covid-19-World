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

            List<CovidData> wordHistory = new List<CovidData>(RestServices.GetDataHistory());

           


            #region ChartCreation
            Chart chart = new Chart()
            {
                Type = Enums.ChartType.Line
            };

            Data data = new Data()
            {
                Labels = new List<string>(),
                Datasets = new List<Dataset>()
                {
                    new LineDataset()
                    {
                        Label = "casi positivi",
                        Data = new List<double?>(),
                        Fill = "true",
                        LineTension = 0.1,
                        BackgroundColor = ChartColor.FromRgba(75, 192, 192, 0.4),
                        BorderColor = ChartColor.FromRgb(75, 192, 192),
                        BorderCapStyle = "butt",
                        BorderDash = new List<int> { },
                        BorderDashOffset = 0.0,
                        BorderJoinStyle = "miter",
                        PointBorderColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                        PointBackgroundColor = new List<ChartColor> { ChartColor.FromHexString("#ffffff") },
                        PointBorderWidth = new List<int> { 1 },
                        PointHoverRadius = new List<int> { 5 },
                        PointHoverBackgroundColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                        PointHoverBorderColor = new List<ChartColor> { ChartColor.FromRgb(220, 220, 220) },
                        PointHoverBorderWidth = new List<int> { 2 },
                        PointRadius = new List<int> { 1 },
                        PointHitRadius = new List<int> { 10 },
                        SpanGaps = false
                    }
                }
            };



            for (int i = 0; i < wordHistory.Count; i++)
            {
                if (i % 48 == 0 || i == wordHistory.Count - 1)
                {
                    data.Datasets[0].Data.Add(int.Parse(wordHistory[i].Cases.Total));
                    if (i % 96 == 0 || i == wordHistory.Count - 1)
                        data.Labels.Add(wordHistory[i].Time.ToString());
                    else
                        data.Labels.Add("");
                }
            }

            data.Datasets[0].Data = data.Datasets[0].Data.Reverse().ToList();
            data.Labels = data.Labels.Reverse().ToList();

            chart.Data = data;

            ViewData["chart"] = chart;
            #endregion



            if (data != null)
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
