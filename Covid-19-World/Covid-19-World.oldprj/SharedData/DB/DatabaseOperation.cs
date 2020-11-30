using Covid_World.EFDataAccessLibrary.DataAccess;
using Covid_World.EFDataAccessLibrary.Models;
using Covid_World.SharedData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static Covid_World.SharedData.Models.CovidDataModel;

namespace Covid19_World.Shared.Models
{
    public static class DatabaseOperation
    {
        public const string WORLD_DEFAULT_CODE = "all";

        /// <summary>
        /// salva i dati di questa lista sul database
        /// </summary>
        /// <param name="History">la lista che il metodo estende</param>
        /// <param name="RefreshData">ti fa scegliere se vuoi aggiornare o meno i dati gia presenti sul database </param>
        public static void SaveOnDatabase(this CovidList<CovidDataModel> History, Covid19wDbContext ContextDB, bool RefreshData = false)
        {
            List<CovidData> PresetData = (from a in ContextDB.CovidDatas select a).ToList();


            foreach (var ConvTable in History.MapModelToDBList())
            {
                CovidData ExistentRow = PresetData.Find(d =>
                    d.Country == ConvTable.Country &&
                    d.Time.Equals(ConvTable.Time));

                if (ExistentRow != null)
                    if (!RefreshData) continue;
                    else ExistentRow.FastCopy(ConvTable);
                else
                    ContextDB.Add(ConvTable);
            }

            ContextDB.SaveChanges();
        }

        public static void FastCopy(this CovidData covidData, CovidData source)
        {
            covidData.CaseNew = source.CaseNew;
            covidData.CaseActive = source.CaseActive;
            covidData.CaseCritical = source.CaseCritical;
            covidData.CaseRecovered = source.CaseRecovered;
            covidData.CaseTotal = source.CaseTotal;
            covidData.DeathNew = source.DeathNew;
            covidData.DeathTotal = source.DeathTotal;
        }

        public static bool IsDataToOld(Covid19wDbContext ContextDB, string Country = WORLD_DEFAULT_CODE)
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

        public static CovidList<CovidDataModel> GetCountryHistory(Covid19wDbContext ContextDB, bool IsHistory = false, string Country = WORLD_DEFAULT_CODE) =>
            new CovidList<CovidDataModel>((from a in ContextDB.CovidDatas where a.Country == Country select a).ToArray().FromDBtoModelArray(), IsHistory);




        public static CovidList<CovidDataModel> GetLastStatsOfCountry(Covid19wDbContext ContextDB, bool IsHistory = false, string Country = WORLD_DEFAULT_CODE)
        {
            var data = ContextDB.CovidDatas.Select(x => x).AsEnumerable();

            var e = (from r in data
                     group r by r.Country into g
                     select new
                     {
                         Country = g.Key,
                         Time = g.Max(x => x.Time)
                     }).ToList();

            var LastData = data
            .Where(r => e.Contains(new { r.Country, r.Time }))
            .Select(r => r)
            .ToArray();

            return new CovidList<CovidDataModel>(LastData.FromDBtoModelArray());
        }
    }
}

    
