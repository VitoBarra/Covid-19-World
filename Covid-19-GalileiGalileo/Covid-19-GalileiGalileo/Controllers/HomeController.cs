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

            CovidList<CovidData> wordHistory = new CovidList<CovidData>(RestServices.GetDataHistory().ToArray());


         



            ViewBag.chartTotalCases = CreateChart("casi positivi", wordHistory.TotalCases(), wordHistory.ListTime(), 41);
            ViewBag.chartNewCases = CreateChart("nuovi casi giornalieri", wordHistory.DiferenceCases(), wordHistory.ListTime(), 79);

            if (wordHistory != null)
                return View(wordHistory[0]);
            else
                return BadRequest();
        }

 


        public static Chart CreateChart(string DatasetName,IList<double?> DataList, IList<string> LabelList, int DataRatio)
        {

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
                        Label = DatasetName,
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



            for (int i = 0; i < DataList.Count; i++)
            {
                if (i % DataRatio == 0)
                {
                    data.Datasets[0].Data.Add(DataList[i]);

                    //if (i % DataRatio == 0)
                        data.Labels.Add(LabelList[i]);
                    //else
                    //    data.Labels.Add("");
                }
            }

            data.Datasets[0].Data = data.Datasets[0].Data.Reverse().ToList();
            data.Labels = data.Labels.Reverse().ToList();

            chart.Data = data;


            return chart;
  

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
