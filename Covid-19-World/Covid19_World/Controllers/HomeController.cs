using Covid_World.EFDataAccessLibrary.DataAccess;
using Covid_World.SharedData.Models;
using Covid19_World.Shared.Models;
using Covid19_World.Shared.Services;
using Covid19_World.Shared.Services.Api;
using Covid19_World.Shared.Services.Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SharedLibrary.AspNetCore.ChartJsTool;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Covid_World.SharedData.Models.CovidDataModel;

namespace Covid_World.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public Covid19wDbContext Covid19WDbContext;

        public IList<CountryPairs> CountryList;

        public HomeController(ILogger<HomeController> logger, Covid19wDbContext covid19WDB, IUtilityFileReader _countryList)
        {
            _logger = logger;
            Covid19WDbContext = covid19WDB;
            CountryList = ((UtilityFileReader)_countryList).CountryList;
        }



        [ResponseCache(Duration = 600, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {

            CovidList<CovidDataModel> worldHistory = DatabaseOperation.GetCountryHistory(Covid19WDbContext, true);

            ViewBag.chartTotalCases = ChartTool.CreateChart(worldHistory.ListTime(),
                new List<ChartData>()
                { new ChartData()
                {
                    DatasetName = "Casi attivi",
                    Data = worldHistory.TotalCases(),
                    ChartPalette = ChartPalette.blue
                }, new ChartData()
                {
                    DatasetName = "Morti",
                    Data = worldHistory.TotalDeaths(),
                    ChartPalette = ChartPalette.blue
                },new ChartData()
                {
                    DatasetName = "Guariti",
                    Data = worldHistory.TotalRecoverd(),
                    ChartPalette = ChartPalette.orange
                }});

            ViewBag.chartNewCases = ChartTool.CreateChart(worldHistory.ListTime().Skip(1).SkipLast(1).ToList(),
            new List<ChartData>() {
                new ChartData()
                {
                    DatasetName = "nuovi casi giornalieri",
                    Data = worldHistory.NewCases(),
                    ChartPalette = ChartPalette.blue
                }, new ChartData()
                {
                    DatasetName = "diferenza di incremento",
                    Data = worldHistory.DiferenceIncrease(),
                    ChartPalette = ChartPalette.orange
                }});



            if (worldHistory.Count != 0)
                return View(worldHistory.Last());
            else
            {
                _logger.LogWarning("Database was Empty");
                return View(new CovidDataModel()
                {
                    Cases = new SharedData.Models.Cases { Active = "0", Critical = "0", New = "0", Recovered = "0", Total = "0" },
                    Deaths = new SharedData.Models.Deaths { New = "0", Total = "0" },
                    Time = new DateTime(),
                    Country = "ERROR"
                });
            }
        }

        [ResponseCache(Duration = 2000, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ContryDic()
        {
            CovidList<CovidDataModel> LastStatOfAllCountry;
            Dictionary<string, string> pairs = new Dictionary<string, string>();


            LastStatOfAllCountry = DatabaseOperation.GetLastStatsOfCountry(Covid19WDbContext);
            foreach (CovidDataModel CovidD in LastStatOfAllCountry)
            {
                    var countrypairs = CountryList.SingleOrDefault(cou => cou.Country == CovidD.Country);
                    if (countrypairs != null && !pairs.ContainsKey(countrypairs.MapCode))
                        pairs.Add(countrypairs.MapCode, CovidD.Cases.Active);
            }


            return new JsonResult(JsonConvert.SerializeObject(pairs));
        }



        [ResponseCache(Duration = 2000, Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult ContryCode() => new JsonResult(JsonConvert.SerializeObject(CountryList));




        [ResponseCache(Duration = 2000, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult CovidStatistic(string Country = "all")
        {
            CovidList<CovidDataModel> CountryHistory = DatabaseOperation.GetCountryHistory(Covid19WDbContext,  true,Country);

            JsonChartDataResponse ChartData = new JsonChartDataResponse()
            {
                ActiveCase = CountryHistory.TotalCases().ToList(),
                TotalDeaths = CountryHistory.TotalDeaths().ToList(),
                TotalRecoverd = CountryHistory.TotalRecoverd().ToList(),
                DailyCases = CountryHistory.NewCases().ToList(),
                DiferenceDailyCases = CountryHistory.DiferenceIncrease().ToList()
            };

            return new JsonResult(JsonConvert.SerializeObject(ChartData));
        }

        [ResponseCache(Duration = 2000, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult AboutMe() => View();



    }
}