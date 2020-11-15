using Covid_World.EFDataAccessLibrary.DataAccess;
using Covid_World.SharedData.Models;
using Covid19_World.Shared.Models;
using Covid19_World.Shared.Services;
using Covid19_World.Shared.Services.Api;
using Covid19_World.Shared.Services.Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedLibrary.AspNetCore.ChartJsTool;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Covid_World.SharedData.Models.CovidDataModel;

namespace Covid_World.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static Covid19wDbContext Covid19WDbContext;

        public HomeController(ILogger<HomeController> logger, Covid19wDbContext covid19WDB)
        {
            _logger = logger;
            Covid19WDbContext = covid19WDB;
        }

        [ResponseCache(Duration = 600, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index()
        {
            CovidList<CovidDataModel> worldHistory;

            if (DatabaseOperation.IsDataToOld(Covid19WDbContext))
            {
                worldHistory = new CovidList<CovidDataModel>(APIListTOModel((await ApiService.GetDataHistory()).ToArray()), true);

                worldHistory.SaveOnDatabase(Covid19WDbContext);
            }
            else
                worldHistory = new CovidList<CovidDataModel>(DatabaseOperation.GetCountryHistory(Covid19WDbContext), true);




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

  


            return View(worldHistory.Last());
        }

        public async Task<IActionResult> ContryDic()
        {
            CovidList<CovidDataModel> LastStatOfallCountry;
            Dictionary<string, int> pairs = new Dictionary<string, int>();

            LastStatOfallCountry = new CovidList<CovidDataModel>(APIListTOModel((await ApiService.GetStatByCountry()).ToArray()));
            LastStatOfallCountry.SaveOnDatabase(Covid19WDbContext);

            using (StreamReader r = new StreamReader("CountryPairs.json"))
            {
                string Json = r.ReadToEnd();
                List<CountryPairs> CountryListr = JsonConvert.DeserializeObject<List<CountryPairs>>(Json);

                foreach (CovidDataModel CovidD in LastStatOfallCountry)
                {
                    var countrypairs = CountryListr.SingleOrDefault(cou => cou.Country == CovidD.Country);
                    if (countrypairs != null)
                        pairs.Add(countrypairs.MapCode, int.Parse(CovidD.Cases.Active));
                }
            }

            return new JsonResult(JsonConvert.SerializeObject(pairs));
        }

        public ActionResult ContryCode()
        {
            List<CountryPairs> CountryListr;

            using (StreamReader r = new StreamReader("CountryPairs.json"))
            {
                string Json = r.ReadToEnd();
                CountryListr = JsonConvert.DeserializeObject<List<CountryPairs>>(Json);
            }

            return new JsonResult(JsonConvert.SerializeObject(CountryListr));
        }

        [ResponseCache(Duration = 600, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> CovidStatistic(string Country = "all")
        {
            CovidList<CovidDataModel> CountryHistory;

            if (DatabaseOperation.IsDataToOld(Covid19WDbContext, Country))
            {
                CountryHistory = new CovidList<CovidDataModel>(APIListTOModel((await ApiService.GetDataHistory(Country)).ToArray()), true);
                CountryHistory.SaveOnDatabase(Covid19WDbContext);
            }
            else
                CountryHistory = new CovidList<CovidDataModel>(DatabaseOperation.GetCountryHistory(Covid19WDbContext, Country), true);

            JsonChartDataResponse ChartData = new JsonChartDataResponse()
            {
                ActiveCase = CountryHistory.TotalCases().ToList(),
                TotalDeaths = CountryHistory.TotalDeaths().ToList(),
                TotalRecoverd = CountryHistory.TotalRecoverd().ToList(),
                DailyCases = CountryHistory.NewCases().ToList(),
                DiferenceDailyCases = CountryHistory.DiferenceIncrease().ToList()
            };

            string jsonStr = JsonConvert.SerializeObject(ChartData);
            return new JsonResult(jsonStr);
        }

        public IActionResult AboutMe()
        {
            return View();
        }

        public IActionResult NewsLetter()
        {
            return View();
        }
    }
}