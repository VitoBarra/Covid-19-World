using ChartJSCore.Models;
using Covid_World.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Covid_World.DBContext
{
    public static class DatabaseOperation
    {
        public static Covid19wDbContext CovidDB = new Covid19wDbContext();
        public static void SaveOnDatabase(this IList<CovidData> History)
        {

            List<Coviddatas> PresetData = (from a in CovidDB.Coviddatas select a).ToList();

            foreach (CovidData CovidDataFromAPI in History)
            {
                Coviddatas TempCovidRow = new Coviddatas();

                TempCovidRow.Country = CovidDataFromAPI.Country;
                TempCovidRow.Time = CovidDataFromAPI.Time.ToString("yyyy/MM/dd H:mm:ss");


                Coviddatas ExistentRow = PresetData.Find(d => d.Country == CovidDataFromAPI.Country && d.Time.Equals(CovidDataFromAPI.Time.ToString("yyyy/MM/dd H:mm:ss")));

                if (ExistentRow != null)
                {
                    ExistentRow.CaseNew = CovidDataFromAPI.Cases.New;
                    ExistentRow.CaseActive = CovidDataFromAPI.Cases.Active;
                    ExistentRow.CaseCritical = CovidDataFromAPI.Cases.Critical;
                    ExistentRow.CaseRecovered = CovidDataFromAPI.Cases.Recovered;
                    ExistentRow.CaseTotal = CovidDataFromAPI.Cases.Total;
                    ExistentRow.DeathNew = CovidDataFromAPI.Deaths.New;
                    ExistentRow.DeathTotal = CovidDataFromAPI.Deaths.Total;
                }
                else
                {
                    TempCovidRow.CaseNew = CovidDataFromAPI.Cases.New;
                    TempCovidRow.CaseActive = CovidDataFromAPI.Cases.Active;
                    TempCovidRow.CaseCritical = CovidDataFromAPI.Cases.Critical;
                    TempCovidRow.CaseRecovered = CovidDataFromAPI.Cases.Recovered;
                    TempCovidRow.CaseTotal = CovidDataFromAPI.Cases.Total;
                    TempCovidRow.DeathNew = CovidDataFromAPI.Deaths.New;
                    TempCovidRow.DeathTotal = CovidDataFromAPI.Deaths.Total;

                    CovidDB.Add(TempCovidRow);

                }

            }

            CovidDB.SaveChanges();
        }


        public static bool IsDataToOld(string Country = "all")
        {
            //return true;

            var LastData = (from a in CovidDB.Coviddatas where a.Country == Country select a.Time).ToList();

            if (LastData != null) return true;

            IList<DateTime> dtlist = new List<DateTime>();

            foreach (string s in LastData)
                dtlist.Add(DateTime.Parse(s));


            DateTime dt = dtlist.Max();
            DateTime nowTime = DateTime.Now;

            if (nowTime > dt.AddMinutes(15))
                return true;
            else
                return false;
        }

    }

  
}
