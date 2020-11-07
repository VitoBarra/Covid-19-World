using Covid_World.EFDataAccessLibrary.DataAccess;
using Covid_World.EFDataAccessLibrary.Models;
using Covid19_World.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19_World.Shared.Models
{
    public static class DatabaseOperation
    {
        /// <summary>
        /// salva i dati di questa lista sul database 
        /// </summary>
        /// <param name="History">la lista che il metodo estende</param>
        /// <param name="RefreshData">ti fa scegliere se vuoi aggiornare o meno i dati gia presenti sul database </param>
        public static void SaveOnDatabase(this IList<CovidDataAPI> History, Covid19wDbContext ContextDB, bool RefreshData = false)
        {

            List<CovidDatas> PresetData = (from a in ContextDB.CovidDatas select a).ToList();

            foreach (CovidDataAPI CovidDataFromAPI in History)
            {
                CovidDatas TempCovidRow = new CovidDatas();

                TempCovidRow.Country = CovidDataFromAPI.Country;
                TempCovidRow.Time = CovidDataFromAPI.Time.ToString("yyyy/MM/dd H:mm:ss");


                CovidDatas ExistentRow = PresetData.Find(d =>
                    d.Country == TempCovidRow.Country &&
                    d.Time.Equals(TempCovidRow.Time));

                if (ExistentRow != null )
                    if(!RefreshData) continue;
                    else TempCovidRow = ExistentRow;
                else
                    ContextDB.Add(TempCovidRow);


                TempCovidRow.CaseNew = CovidDataFromAPI.Cases.New;
                TempCovidRow.CaseActive = CovidDataFromAPI.Cases.Active;
                TempCovidRow.CaseCritical = CovidDataFromAPI.Cases.Critical;
                TempCovidRow.CaseRecovered = CovidDataFromAPI.Cases.Recovered;
                TempCovidRow.CaseTotal = CovidDataFromAPI.Cases.Total;
                TempCovidRow.DeathNew = CovidDataFromAPI.Deaths.New;
                TempCovidRow.DeathTotal = CovidDataFromAPI.Deaths.Total;
            }


            ContextDB.SaveChanges();
        }



        public static bool IsDataToOld(Covid19wDbContext ContextDB,string Country = "all")
        {
            var LastData = (from a in ContextDB.CovidDatas where a.Country == Country select a.Time).ToList();

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

        public static CovidDataAPI[] GetCountryHistory(Covid19wDbContext ContextDB,string Country = "all") => 
            (from a in ContextDB.CovidDatas where a.Country == Country select a).ToArray().FromDBtoAPI();
    }


    public static class CovidEx
    {
        /// <summary>
        /// converte il formato database dei dati covid al formato API utilizato nel programma
        /// </summary>
        /// <param name="coviddatas"></param>
        /// <returns></returns>
        public static CovidDataAPI[] FromDBtoAPI(this CovidDatas[] coviddatas)
        {

            List<CovidDataAPI> cd = new List<CovidDataAPI>();

            IEnumerable<CovidDatas> enume = coviddatas.OrderBy(a => a.Time).Reverse();


            foreach (CovidDatas cs in enume)
            {
                CovidDataAPI te = new CovidDataAPI()
                {
                    Cases = new Cases
                    {
                        Active = cs.CaseActive,
                        New = cs.CaseNew,
                        Critical = cs.CaseCritical,
                        Recovered = cs.CaseRecovered,
                        Total = cs.CaseTotal
                    },
                    Deaths = new Deaths { New = cs.DeathNew, Total = cs.DeathTotal },
                    Country = cs.Country,
                    Time = DateTime.Parse(cs.Time)
                };
                cd.Add(te);
            }

            return cd.ToArray();
        }
    }

}
