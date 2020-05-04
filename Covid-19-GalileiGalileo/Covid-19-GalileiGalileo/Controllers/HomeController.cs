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
using Newtonsoft.Json;

namespace Covid_19_GalileiGalileo.Controllers
{
    public class HomeController : Controller
    {
        Dictionary<string, int> pairs = new Dictionary<string, int>();


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            CovidList<CovidData> worldHistory;
            try
            {
                RestServices.StartUpAPI();
                worldHistory = new CovidList<CovidData>(RestServices.GetDataHistory().ToArray(), true);
            } catch (Exception e)
            {
                return Error();
            }

            ViewBag.chartTotalCases = ChartTool.CreateChart(worldHistory.ListTime(),
                new List<ChartData>() { new ChartData() 
                {
                    DatasetName = "casi positivi",
                    Data = worldHistory.TotalCases()
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



            foreach (CovidData cd in new CovidList<CovidData>(RestServices.GetStatByCountry().ToArray()))
            {
                if (cd.Country.Equals("China"))
                {
                    pairs.Add("cn", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Italy"))
                {
                    pairs.Add("it", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Spain"))
                {
                    pairs.Add("es", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("USA"))
                {
                    pairs.Add("us", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Germany"))
                {
                    pairs.Add("de", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Iran"))
                {
                    pairs.Add("ir", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("France"))
                {
                    pairs.Add("fr", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("S-Korea"))
                {
                    pairs.Add("kr", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Switzerland"))
                {
                    pairs.Add("ch", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("UK"))
                {
                    pairs.Add("gb", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Netherlands"))
                {
                    pairs.Add("nl", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Austria"))
                {
                    pairs.Add("at", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Belgium"))
                {
                    pairs.Add("be", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Norway"))
                {
                    pairs.Add("no", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Sweden"))
                {
                    pairs.Add("se", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Canada"))
                {
                    pairs.Add("ca", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Denmark"))
                {
                    pairs.Add("dk", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Portugal"))
                {
                    pairs.Add("pt", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Malaysia"))
                {
                    pairs.Add("my", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Brazil"))
                {
                    pairs.Add("br", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Australia"))
                {
                    pairs.Add("au", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Japan"))
                {
                    pairs.Add("jp", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Czechia"))
                {
                    pairs.Add("cz", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Turkey"))
                {
                    pairs.Add("tr", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Israel"))
                {
                    pairs.Add("il", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Ireland"))
                {
                    pairs.Add("ie", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Pakistan"))
                {
                    pairs.Add("pk", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Chile"))
                {
                    pairs.Add("cl", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Poland"))
                {
                    pairs.Add("pl", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Ecuador"))
                {
                    pairs.Add("ec", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Greece"))
                {
                    pairs.Add("gr", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Finland"))
                {
                    pairs.Add("fi", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Qatar"))
                {
                    pairs.Add("qa", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Iceland"))
                {
                    pairs.Add("is", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Indonesia"))
                {
                    pairs.Add("id", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Thailand"))
                {
                    pairs.Add("th", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Saudi-Arabia"))
                {
                    pairs.Add("sa", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Slovenia"))
                {
                    pairs.Add("si", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Romania"))
                {
                    pairs.Add("ro", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("India"))
                {
                    pairs.Add("in", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Peru"))
                {
                    pairs.Add("pe", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Philippines"))
                {
                    pairs.Add("ph", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Russia"))
                {
                    pairs.Add("ru", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Estonia"))
                {
                    pairs.Add("ee", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Egypt"))
                {
                    pairs.Add("eg", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("South-Africa"))
                {
                    pairs.Add("za", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Lebanon"))
                {
                    pairs.Add("lb", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Iraq"))
                {
                    pairs.Add("iq", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Croatia"))
                {
                    pairs.Add("hr", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Mexico"))
                {
                    pairs.Add("mx", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Panama"))
                {
                    pairs.Add("pa", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Colombia"))
                {
                    pairs.Add("co", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Slovakia"))
                {
                    pairs.Add("sk", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Kuwait"))
                {
                    pairs.Add("kw", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Serbia"))
                {
                    pairs.Add("rs", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Bulgaria"))
                {
                    pairs.Add("bg", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Armenia"))
                {
                    pairs.Add("am", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Argentina"))
                {
                    pairs.Add("ar", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Taiwan"))
                {
                    pairs.Add("tw", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("UAE"))
                {
                    pairs.Add("ae", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Algeria"))
                {
                    pairs.Add("dz", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Latvia"))
                {
                    pairs.Add("lv", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Costa-Rica"))
                {
                    pairs.Add("cr", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Dominican-Republic"))
                {
                    pairs.Add("do", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Uruguay"))
                {
                    pairs.Add("uy", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Hungary"))
                {
                    pairs.Add("hu", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Jordan"))
                {
                    pairs.Add("jo", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Lithuania"))
                {
                    pairs.Add("lt", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Morocco"))
                {
                    pairs.Add("ma", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Vietnam"))
                {
                    pairs.Add("vn", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Bosnia-and-Herzegovina"))
                {
                    pairs.Add("ba", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("North-Macedonia"))
                {
                    pairs.Add("mk", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Cyprus"))
                {
                    pairs.Add("cy", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Brunei"))
                {
                    pairs.Add("bn", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Moldova"))
                {
                    pairs.Add("md", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Sri-Lanka"))
                {
                    pairs.Add("lk", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Albania"))
                {
                    pairs.Add("al", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Belarus"))
                {
                    pairs.Add("by", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Malta"))
                {
                    pairs.Add("mt", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Venezuela"))
                {
                    pairs.Add("ve", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Burkina-Faso"))
                {
                    pairs.Add("bf", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Tunisia"))
                {
                    pairs.Add("tn", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Senegal"))
                {
                    pairs.Add("sn", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Kazakhstan"))
                {
                    pairs.Add("kz", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Azerbaijan"))
                {
                    pairs.Add("az", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Cambodia"))
                {
                    pairs.Add("kh", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("New-Zealand"))
                {
                    pairs.Add("nz", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Oman"))
                {
                    pairs.Add("om", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Georgia"))
                {
                    pairs.Add("ge", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Trinidad-and-Tobago"))
                {
                    pairs.Add("tt", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Ukraine"))
                {
                    pairs.Add("ua", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Uzbekistan"))
                {
                    pairs.Add("uz", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Cameroon"))
                {
                    pairs.Add("cm", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Bangladesh"))
                {
                    pairs.Add("bd", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Afghanistan"))
                {
                    pairs.Add("af", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Honduras"))
                {
                    pairs.Add("hn", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Nigeria"))
                {
                    pairs.Add("ng", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Cuba"))
                {
                    pairs.Add("cu", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Ghana"))
                {
                    pairs.Add("gh", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Jamaica"))
                {
                    pairs.Add("jm", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Bolivia"))
                {
                    pairs.Add("bo", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Guyana"))
                {
                    pairs.Add("gy", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Paraguay"))
                {
                    pairs.Add("py", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("French-Guiana"))
                {
                    pairs.Add("gf", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Guatemala"))
                {
                    pairs.Add("gt", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Rwanda"))
                {
                    pairs.Add("rw", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Togo"))
                {
                    pairs.Add("tg", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("French-Polynesia"))
                {
                    pairs.Add("pf", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Mauritius"))
                {
                    pairs.Add("mu", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Barbados"))
                {
                    pairs.Add("bb", int.Parse(cd.Cases.Active));
                }  
                else if (cd.Country.Equals("Maldives"))
                {
                    pairs.Add("mv", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Mongolia"))
                {
                    pairs.Add("mn", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Ethiopia"))
                {
                    pairs.Add("et", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Kenya"))
                {
                    pairs.Add("ke", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Seychelles"))
                {
                    pairs.Add("sc", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Equatorial-Guinea"))
                {
                    pairs.Add("gq", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Tanzania"))
                {
                    pairs.Add("tz", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Gabon"))
                {
                    pairs.Add("ga", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Suriname"))
                {
                    pairs.Add("sr", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Bahamas"))
                {
                    pairs.Add("bs", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("New-Caledonia"))
                {
                    pairs.Add("nc", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Cabo-Verde"))
                {
                    pairs.Add("cv", int.Parse(cd.Cases.Active));
                }

                else if (cd.Country.Equals("Congo"))
                {
                    pairs.Add("cg", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("El-Salvador"))
                {
                    pairs.Add("sv", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Liberia"))
                {
                    pairs.Add("lr", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Madagascar"))
                {
                    pairs.Add("mg", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Namibia"))
                {
                    pairs.Add("na", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Zimbabwe"))
                {
                    pairs.Add("zw", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Sudan"))
                {
                    pairs.Add("sd", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Angola"))
                {
                    pairs.Add("ao", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Benin"))
                {
                    pairs.Add("bj", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Fiji"))
                {
                    pairs.Add("fj", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Greenland"))
                {
                    pairs.Add("gl", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Guinea"))
                {
                    pairs.Add("gn", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Haiti"))
                {
                    pairs.Add("ht", int.Parse(cd.Cases.Active));
                }

                else if (cd.Country.Equals("Mauritania"))
                {
                    pairs.Add("mr", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Nicaragua"))
                {
                    pairs.Add("ni", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Saint-Lucia"))
                {
                    pairs.Add("lc", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Zambia"))
                {
                    pairs.Add("zm", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Nepal"))
                {
                    pairs.Add("np", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Antigua-and-Barbuda"))
                {
                    pairs.Add("ag", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Chad"))
                {
                    pairs.Add("td", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Djibouti"))
                {
                    pairs.Add("dj", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Eritrea"))
                {
                    pairs.Add("er", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Gambia"))
                {
                    pairs.Add("gm", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Niger"))
                {
                    pairs.Add("ne", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Papua-New-Guinea"))
                {
                    pairs.Add("pg", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Somalia"))
                {
                    pairs.Add("so", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Timor-Leste"))
                {
                    pairs.Add("tl", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Uganda"))
                {
                    pairs.Add("ug", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Mozambique"))
                {
                    pairs.Add("mz", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Syria"))
                {
                    pairs.Add("sy", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Grenada"))
                {
                    pairs.Add("gd", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Dominica"))
                {
                    pairs.Add("dm", int.Parse(cd.Cases.Active));
                }

                else if (cd.Country.Equals("Belize"))
                {
                    pairs.Add("bz", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Myanmar"))
                {
                    pairs.Add("mm", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Libya"))
                {
                    pairs.Add("ly", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Mali"))
                {
                    pairs.Add("ml", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Guinea-Bissau"))
                {
                    pairs.Add("gw", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Saint-Kitts-and-Nevis"))
                {
                    pairs.Add("kn", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Botswana"))
                {
                    pairs.Add("bw", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Sierra-Leone"))
                {
                    pairs.Add("sl", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Burundi"))
                {
                    pairs.Add("bi", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Malawi"))
                {
                    pairs.Add("mw", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Falkland-Islands"))
                {
                    pairs.Add("fk", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Sao-Tome-and-Principe"))
                {
                    pairs.Add("st", int.Parse(cd.Cases.Active));
                }
                else if (cd.Country.Equals("Yemen"))
                {
                    pairs.Add("ye", int.Parse(cd.Cases.Active));
                }
            }


            ViewBag.CountryValue = pairs;


            if (worldHistory != null)
                return View(worldHistory[0]);
            else
                return BadRequest();
        }



        public IActionResult CovidStatistic(string Country = "all")
        {
            CovidList<CovidData> List = new CovidList<CovidData>(RestServices.GetDataHistory(Country).ToArray(), true);
                IList<string> s = new List<string>();
            foreach (double? d in List.TotalCases())
                s.Add(d.ToString());
 
            return new JsonResult(JsonConvert.SerializeObject(s));
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
