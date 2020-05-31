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
                    ChartPalette = ChartPalette.violette
                },new ChartData()
                {
                    DatasetName = "Guariti",
                    Data = worldHistory.TotalRecoverd(),
                    ChartPalette = ChartPalette.orange
                }}) ;


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



            foreach (CovidData CovidD in new CovidList<CovidData>(RestServices.GetStatByCountry().ToArray()))
            {
                if (CovidD.Country.Equals("China"))
                {
                    pairs.Add("cn", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Italy"))
                {
                    pairs.Add("it", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Spain"))
                {
                    pairs.Add("es", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("USA"))
                {
                    pairs.Add("us", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Germany"))
                {
                    pairs.Add("de", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Iran"))
                {
                    pairs.Add("ir", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("France"))
                {
                    pairs.Add("fr", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("S-Korea"))
                {
                    pairs.Add("kr", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Switzerland"))
                {
                    pairs.Add("ch", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("UK"))
                {
                    pairs.Add("gb", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Netherlands"))
                {
                    pairs.Add("nl", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Austria"))
                {
                    pairs.Add("at", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Belgium"))
                {
                    pairs.Add("be", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Norway"))
                {
                    pairs.Add("no", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Sweden"))
                {
                    pairs.Add("se", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Canada"))
                {
                    pairs.Add("ca", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Denmark"))
                {
                    pairs.Add("dk", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Portugal"))
                {
                    pairs.Add("pt", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Malaysia"))
                {
                    pairs.Add("my", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Brazil"))
                {
                    pairs.Add("br", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Australia"))
                {
                    pairs.Add("au", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Japan"))
                {
                    pairs.Add("jp", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Czechia"))
                {
                    pairs.Add("cz", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Turkey"))
                {
                    pairs.Add("tr", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Israel"))
                {
                    pairs.Add("il", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Ireland"))
                {
                    pairs.Add("ie", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Pakistan"))
                {
                    pairs.Add("pk", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Chile"))
                {
                    pairs.Add("cl", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Poland"))
                {
                    pairs.Add("pl", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Ecuador"))
                {
                    pairs.Add("ec", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Greece"))
                {
                    pairs.Add("gr", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Finland"))
                {
                    pairs.Add("fi", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Qatar"))
                {
                    pairs.Add("qa", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Iceland"))
                {
                    pairs.Add("is", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Indonesia"))
                {
                    pairs.Add("id", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Thailand"))
                {
                    pairs.Add("th", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Saudi-Arabia"))
                {
                    pairs.Add("sa", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Slovenia"))
                {
                    pairs.Add("si", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Romania"))
                {
                    pairs.Add("ro", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("India"))
                {
                    pairs.Add("in", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Peru"))
                {
                    pairs.Add("pe", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Philippines"))
                {
                    pairs.Add("ph", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Russia"))
                {
                    pairs.Add("ru", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Estonia"))
                {
                    pairs.Add("ee", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Egypt"))
                {
                    pairs.Add("eg", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("South-Africa"))
                {
                    pairs.Add("za", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Lebanon"))
                {
                    pairs.Add("lb", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Iraq"))
                {
                    pairs.Add("iq", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Croatia"))
                {
                    pairs.Add("hr", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Mexico"))
                {
                    pairs.Add("mx", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Panama"))
                {
                    pairs.Add("pa", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Colombia"))
                {
                    pairs.Add("co", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("DRC"))
                {
                    pairs.Add("cd", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Slovakia"))
                {
                    pairs.Add("sk", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Kuwait"))
                {
                    pairs.Add("kw", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Serbia"))
                {
                    pairs.Add("rs", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Bulgaria"))
                {
                    pairs.Add("bg", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Armenia"))
                {
                    pairs.Add("am", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Argentina"))
                {
                    pairs.Add("ar", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Taiwan"))
                {
                    pairs.Add("tw", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("UAE"))
                {
                    pairs.Add("ae", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Algeria"))
                {
                    pairs.Add("dz", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Latvia"))
                {
                    pairs.Add("lv", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Costa-Rica"))
                {
                    pairs.Add("cr", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Dominican-Republic"))
                {
                    pairs.Add("do", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Uruguay"))
                {
                    pairs.Add("uy", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Hungary"))
                {
                    pairs.Add("hu", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Jordan"))
                {
                    pairs.Add("jo", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Lithuania"))
                {
                    pairs.Add("lt", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Morocco"))
                {
                    pairs.Add("ma", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Vietnam"))
                {
                    pairs.Add("vn", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Bosnia-and-Herzegovina"))
                {
                    pairs.Add("ba", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("North-Macedonia"))
                {
                    pairs.Add("mk", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Cyprus"))
                {
                    pairs.Add("cy", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Brunei"))
                {
                    pairs.Add("bn", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Moldova"))
                {
                    pairs.Add("md", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Sri-Lanka"))
                {
                    pairs.Add("lk", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Albania"))
                {
                    pairs.Add("al", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Belarus"))
                {
                    pairs.Add("by", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Malta"))
                {
                    pairs.Add("mt", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Venezuela"))
                {
                    pairs.Add("ve", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Burkina-Faso"))
                {
                    pairs.Add("bf", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Tunisia"))
                {
                    pairs.Add("tn", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Senegal"))
                {
                    pairs.Add("sn", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Kazakhstan"))
                {
                    pairs.Add("kz", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Azerbaijan"))
                {
                    pairs.Add("az", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Cambodia"))
                {
                    pairs.Add("kh", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("New-Zealand"))
                {
                    pairs.Add("nz", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Oman"))
                {
                    pairs.Add("om", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Georgia"))
                {
                    pairs.Add("ge", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Trinidad-and-Tobago"))
                {
                    pairs.Add("tt", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Ukraine"))
                {
                    pairs.Add("ua", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Uzbekistan"))
                {
                    pairs.Add("uz", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Cameroon"))
                {
                    pairs.Add("cm", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Bangladesh"))
                {
                    pairs.Add("bd", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Afghanistan"))
                {
                    pairs.Add("af", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Honduras"))
                {
                    pairs.Add("hn", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Nigeria"))
                {
                    pairs.Add("ng", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Cuba"))
                {
                    pairs.Add("cu", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Ghana"))
                {
                    pairs.Add("gh", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Jamaica"))
                {
                    pairs.Add("jm", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Bolivia"))
                {
                    pairs.Add("bo", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Guyana"))
                {
                    pairs.Add("gy", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Paraguay"))
                {
                    pairs.Add("py", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("French-Guiana"))
                {
                    pairs.Add("gf", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Guatemala"))
                {
                    pairs.Add("gt", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Rwanda"))
                {
                    pairs.Add("rw", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Togo"))
                {
                    pairs.Add("tg", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("French-Polynesia"))
                {
                    pairs.Add("pf", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Mauritius"))
                {
                    pairs.Add("mu", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Barbados"))
                {
                    pairs.Add("bb", int.Parse(CovidD.Cases.Active));
                }  
                else if (CovidD.Country.Equals("Maldives"))
                {
                    pairs.Add("mv", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Mongolia"))
                {
                    pairs.Add("mn", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Ethiopia"))
                {
                    pairs.Add("et", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Kenya"))
                {
                    pairs.Add("ke", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Seychelles"))
                {
                    pairs.Add("sc", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Equatorial-Guinea"))
                {
                    pairs.Add("gq", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Tanzania"))
                {
                    pairs.Add("tz", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Gabon"))
                {
                    pairs.Add("ga", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Suriname"))
                {
                    pairs.Add("sr", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Bahamas"))
                {
                    pairs.Add("bs", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("New-Caledonia"))
                {
                    pairs.Add("nc", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Cabo-Verde"))
                {
                    pairs.Add("cv", int.Parse(CovidD.Cases.Active));
                }

                else if (CovidD.Country.Equals("Congo"))
                {
                    pairs.Add("cg", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("El-Salvador"))
                {
                    pairs.Add("sv", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Liberia"))
                {
                    pairs.Add("lr", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Madagascar"))
                {
                    pairs.Add("mg", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Namibia"))
                {
                    pairs.Add("na", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Zimbabwe"))
                {
                    pairs.Add("zw", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Sudan"))
                {
                    pairs.Add("sd", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Angola"))
                {
                    pairs.Add("ao", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Benin"))
                {
                    pairs.Add("bj", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Fiji"))
                {
                    pairs.Add("fj", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Greenland"))
                {
                    pairs.Add("gl", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Guinea"))
                {
                    pairs.Add("gn", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Haiti"))
                {
                    pairs.Add("ht", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Mauritania"))
                {
                    pairs.Add("mr", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Nicaragua"))
                {
                    pairs.Add("ni", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Saint-Lucia"))
                {
                    pairs.Add("lc", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Zambia"))
                {
                    pairs.Add("zm", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Nepal"))
                {
                    pairs.Add("np", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Antigua-and-Barbuda"))
                {
                    pairs.Add("ag", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Chad"))
                {
                    pairs.Add("td", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Djibouti"))
                {
                    pairs.Add("dj", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Eritrea"))
                {
                    pairs.Add("er", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Gambia"))
                {
                    pairs.Add("gm", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Niger"))
                {
                    pairs.Add("ne", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Papua-New-Guinea"))
                {
                    pairs.Add("pg", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Somalia"))
                {
                    pairs.Add("so", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Timor-Leste"))
                {
                    pairs.Add("tl", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Uganda"))
                {
                    pairs.Add("ug", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Mozambique"))
                {
                    pairs.Add("mz", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Syria"))
                {
                    pairs.Add("sy", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Grenada"))
                {
                    pairs.Add("gd", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Dominica"))
                {
                    pairs.Add("dm", int.Parse(CovidD.Cases.Active));
                }

                else if (CovidD.Country.Equals("Belize"))
                {
                    pairs.Add("bz", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Myanmar"))
                {
                    pairs.Add("mm", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Libya"))
                {
                    pairs.Add("ly", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Mali"))
                {
                    pairs.Add("ml", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Guinea-Bissau"))
                {
                    pairs.Add("gw", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Saint-Kitts-and-Nevis"))
                {
                    pairs.Add("kn", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Botswana"))
                {
                    pairs.Add("bw", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Sierra-Leone"))
                {
                    pairs.Add("sl", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Burundi"))
                {
                    pairs.Add("bi", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Malawi"))
                {
                    pairs.Add("mw", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Falkland-Islands"))
                {
                    pairs.Add("fk", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Sao-Tome-and-Principe"))
                {
                    pairs.Add("st", int.Parse(CovidD.Cases.Active));
                }
                else if (CovidD.Country.Equals("Yemen"))
                {
                    pairs.Add("ye", int.Parse(CovidD.Cases.Active));
                }
            }


            ViewBag.CountryValue = pairs;


            if (worldHistory != null)
                return View(worldHistory.Last());
            else
                return BadRequest();
        }



        public IActionResult CovidStatistic(string Country = "all")
        {
            CovidList<CovidData> CountryHistory = new CovidList<CovidData>(RestServices.GetDataHistory(Country).ToArray(), true);
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


        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult AboutMe()
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
