using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Covid_World.Services;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.IO;
using SharedLibrary.AspNetCore.ChartJsTool;
using Covid_World.EFDataAccessLibrary.DataAccess;
using Covid19_World.Shared.Models;

namespace Covid_World.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static Covid19wDbContext Covid19WDbContext;

        public HomeController(ILogger<HomeController> logger, Covid19wDbContext covid19WDB )
        {
            _logger = logger;
            Covid19WDbContext = covid19WDB;
        }


        [ResponseCache(Duration = 600, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            CovidList<CovidDataAPI> worldHistory;
            ErrorAPI ErrorAPIHendler = new ErrorAPI();

            if (DatabaseOperation.IsDataToOld(Covid19WDbContext))
            {
                try
                {
                    worldHistory = new CovidList<CovidDataAPI>(RestServices.GetDataHistory(out ErrorAPIHendler.httpResponse).ToArray(), true);
                }
                catch (Exception e)
                {
                    ErrorAPIHendler.Description = $"questo errore viene dal recupero iniziale dei dati: {e.Message} || code: {ErrorAPIHendler.HttpResponse.StatusCode}";
                    return RedirectToAction("APIError", ErrorAPIHendler);
                }
                worldHistory.SaveOnDatabase(Covid19WDbContext);

            }
            else
                worldHistory = new CovidList<CovidDataAPI>(DatabaseOperation.GetCountryHistory(Covid19WDbContext), true);




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









            IActionResult actionResult;

            ViewBag.CountryValue = GetCountryValue(out actionResult);
            if (ViewBag.CountryValue == null)
                return actionResult;


            return View(worldHistory.Last());
        }




        public Dictionary<string, int> GetCountryValue(out IActionResult ErrorStatus)
        {
            CovidList<CovidDataAPI> LastStatOfallCountry;
            Dictionary<string, int> pairs = new Dictionary<string, int>();
            ErrorStatus = null;

            ErrorAPI ErrorAPIHendler = new ErrorAPI();

            try
            {
                LastStatOfallCountry = new CovidList<CovidDataAPI>(RestServices.GetStatByCountry(out ErrorAPIHendler.httpResponse).ToArray());
            }
            catch
            {
                ErrorAPIHendler.Description = "questo errore viene dala generazione della lista di associazione dei valori";
                ErrorStatus = RedirectToAction("APIError", ErrorAPIHendler);
                return null;
            }
            LastStatOfallCountry.SaveOnDatabase(Covid19WDbContext);




            using (StreamReader r = new StreamReader("CountryPairs.json"))
            {
                string Json = r.ReadToEnd();
                List<CountryPairs> CountryListr = JsonConvert.DeserializeObject<List<CountryPairs>>(Json);

                foreach (CovidDataAPI CovidD in LastStatOfallCountry)
                {
                    var countrypairs = CountryListr.SingleOrDefault(cou => cou.Country.Equals(CovidD.Country));
                    if (countrypairs != null)
                        pairs.Add(countrypairs.MapCode, int.Parse(CovidD.Cases.Active));
                }
            }
            return pairs;
        }



        [ResponseCache(Duration = 600, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult CovidStatistic(string Country = "all")
        {

            CovidList<CovidDataAPI> CountryHistory;


            if (DatabaseOperation.IsDataToOld(Covid19WDbContext, Country))
            {
                ErrorAPI ErrorAPIhendler = new ErrorAPI();
                try
                {
                    CountryHistory = new CovidList<CovidDataAPI>(RestServices.GetDataHistory(out ErrorAPIhendler.httpResponse, Country).ToArray(), true);
                }
                catch
                {
                    ErrorAPIhendler.Description = "questo errore viene dal click di una regione";
                    return RedirectToAction("APIError", ErrorAPIhendler);
                }
                CountryHistory.SaveOnDatabase(Covid19WDbContext);
            }
            else
                CountryHistory = new CovidList<CovidDataAPI>(DatabaseOperation.GetCountryHistory(Covid19WDbContext, Country), true);



            JsonChartDataResponse ChartData = new JsonChartDataResponse()
            {
                ActiveCase = CountryHistory.TotalCases().ToList(),
                TotalDeaths = CountryHistory.TotalDeaths().ToList(),
                TotalRecoverd = CountryHistory.TotalRecoverd().ToList(),
                DailyCases = CountryHistory.NewCases().ToList(),
                DiferenceDailyCases = CountryHistory.DiferenceIncrease().ToList()
            };

            string jsonStr = JsonConvert.SerializeObject(ChartData, Formatting.Indented);
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

        public IActionResult APIError(ErrorAPI s)
        {
            return View(s);
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
