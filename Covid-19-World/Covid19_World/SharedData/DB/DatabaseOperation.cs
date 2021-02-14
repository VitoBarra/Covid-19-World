using Covid_World.EFDataAccessLibrary.DataAccess;
using Covid_World.EFDataAccessLibrary.Models;
using Covid_World.SharedData.Models;
using Covid19_World.Shared.Services.Api;
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
                    d.Time == ConvTable.Time);

                if (ExistentRow != null)
                    if (!RefreshData) continue;
                    else FastCopy(ExistentRow, ConvTable);
                else
                    ContextDB.Add(ConvTable);
            }

            ContextDB.SaveChanges();

            static void FastCopy(CovidData covidData, CovidData source)
            {
                covidData.CaseNew = source.CaseNew;
                covidData.CaseActive = source.CaseActive;
                covidData.CaseRecovered = source.CaseRecovered;
                covidData.CaseTotal = source.CaseTotal;
                covidData.DeathNew = source.DeathNew;
                covidData.DeathTotal = source.DeathTotal;
            }
        }



        public static CovidList<CovidDataModel> GetCountryHistory(this Covid19wDbContext ContextDB, bool IsHistory = false, string CountrySlug = WORLD_DEFAULT_CODE)
        {
            var History = ContextDB.CovidDatas.Where(x=> x.Country == CountrySlug).Select(x=>x).ToArray().FromDBtoModelArray();

            if (History == null)
                History = ApiService.GetDataHistory(CountrySlug).FromAPItoModelArray();


            return new CovidList<CovidDataModel>(History, IsHistory);
        }


        public static CovidList<CovidDataModel> GenerateSumOfAll(this Covid19wDbContext ContextDB)
        {
            var data = ContextDB.CovidDatas.Select(x => x).AsEnumerable();

            var LastData = (from r in data
                            group r by r.Time into g
                            select new CovidData
                            {
                                Time = g.Key,
                                CaseActive = g.Sum(x => { if (x.CaseActive != null) return int.Parse(x.CaseActive); else return null; }).ToString(),
                                CaseNew = g.Sum(x => { if (x.CaseActive != null) return int.Parse(x.CaseNew); else return null; }).ToString(),
                                DeathTotal = g.Sum(x => { if (x.CaseActive != null) return int.Parse(x.DeathTotal); else return null; }).ToString(),
                                DeathNew = g.Sum(x => { if (x.CaseActive != null) return int.Parse(x.DeathNew); else return null; }).ToString(),
                                CaseTotal = g.Sum(x => { if (x.CaseTotal != null) return int.Parse(x.CaseTotal); else return null; }).ToString(),
                                CaseRecovered = g.Sum(x => { if (x.CaseRecovered != null) return int.Parse(x.CaseRecovered); else return null; }).ToString(),
                                Country = "All"

                            }).ToArray();

            var CovidALL = new CovidList<CovidDataModel>(LastData.FromDBtoModelArray());
            CovidALL.FillNewCase();
            return CovidALL;
        }




        public static CovidList<CovidDataModel> GetLastStatsOfCountry( this Covid19wDbContext ContextDB)
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

    
