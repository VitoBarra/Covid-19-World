using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Covid_World.Models;
using Covid_World.Services;
using ChartJSCore.Models;
using ChartJSCore.Helpers;
using Covid_World.Tool;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Covid_World.DBContext;
using System.IO;
using MySql.Data.EntityFrameworkCore.Query.Internal;

namespace Covid_World.Controllers
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
            CovidList<CovidData> worldHistory;

            if (DatabaseOperation.IsDataToOld())
            {
                ErrorAPI ErrorAPIHendler = new ErrorAPI();
                try
                {
                    worldHistory = new CovidList<CovidData>(RestServices.GetDataHistory(out ErrorAPIHendler.HttpResponse).ToArray(), true);
                }
                catch (Exception e)
                {
                    ErrorAPIHendler.Description = $"questo errore viene dal recupero iniziale dei dati: {e.Message}";
                    return RedirectToAction("APIError", ErrorAPIHendler);
                }
                worldHistory.SaveOnDatabase();

            }
            else
                worldHistory = new CovidList<CovidData>((from a in DatabaseOperation.CovidDB.Coviddatas where a.Country.Equals("all") select a).ToArray().CovidToArray(), true);




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
            CovidList<CovidData> LastStatOfallCountry;
            Dictionary<string, int> pairs = new Dictionary<string, int>();
            ErrorStatus = null;

            using (ErrorAPI ErrorAPIHendler = new ErrorAPI())
            {
                try
                {
                    LastStatOfallCountry = new CovidList<CovidData>(RestServices.GetStatByCountry(out ErrorAPIHendler.HttpResponse).ToArray());
                }
                catch
                {
                    ErrorAPIHendler.Description = "questo errore viene dala generazione della lista di associazione dei valori";
                    ErrorStatus = RedirectToAction("APIError", ErrorAPIHendler);
                    return null;
                }
                LastStatOfallCountry.SaveOnDatabase();
            }



            using (StreamReader r = new StreamReader("CountryPairs.json"))
            {
                string Json = r.ReadToEnd();
                List<CountryPairs> CountryListr = JsonConvert.DeserializeObject<List<CountryPairs>>(Json);

                foreach (CovidData CovidD in LastStatOfallCountry)
                {
                    var countrypairs = CountryListr.SingleOrDefault(cou => cou.Country.Equals(CovidD.Country));
                    if (countrypairs != null)
                        pairs.Add(countrypairs.MapCode, int.Parse(CovidD.Cases.Active));
                }
            }
            return pairs;
        }




        public IActionResult CovidStatistic(string Country = "all")
        {

            CovidList<CovidData> CountryHistory;


            if (DatabaseOperation.IsDataToOld(Country))
            {
                ErrorAPI ErrorAPIhendler = new ErrorAPI();
                try
                {
                    CountryHistory = new CovidList<CovidData>(RestServices.GetDataHistory(out ErrorAPIhendler.HttpResponse, Country).ToArray(), true);
                }
                catch
                {
                    ErrorAPIhendler.Description = "questo errore viene dal click di una regione";
                    return RedirectToAction("APIError", ErrorAPIhendler);
                }
                    CountryHistory.SaveOnDatabase();
            }
            else
                CountryHistory = new CovidList<CovidData>((from a in DatabaseOperation.CovidDB.Coviddatas where a.Country.Equals(Country) select a).ToArray().CovidToArray(), true);



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
