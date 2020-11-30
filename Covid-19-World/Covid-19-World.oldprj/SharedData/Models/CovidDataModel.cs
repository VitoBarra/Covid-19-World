using Covid_World.EFDataAccessLibrary.Models;
using Covid_World.SharedData.Models;
using Covid19_World.Shared.Models;
using Covid19_World.Shared.Services.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid_World.SharedData.Models
{
    [Serializable]
    public class Cases :ICloneable
    {
        public string New { get; set; }

        public string Active { get; set; }

        public string Critical { get; set; }

        public string Recovered { get; set; }

        public string Total { get; set; }

        public object Clone()
        {
            return new Cases
            {
                New = this.New,
                Active = this.Active,
                Critical = this.Critical,
                Recovered = this.Recovered,
                Total = this.Total
            };
        }
    }
    public class Deaths :ICloneable
    {
        public string New { get; set; }

        public string Total { get; set; }


        public object Clone()
        {
            return new Deaths
            {
                New = this.New,
                Total = this.Total,
            };
        }
    }

    public class CovidDataModel :ICloneable
    {


        public string Country { get; set; }

        public Cases Cases { get; set; }

        public Deaths Deaths { get; set; }

        public DateTime Time { get; set; }

        public CovidDataModel(){}

        public CovidDataModel(CovidDataAPI dataAPI)
        {
            Country = dataAPI.Country;
            Cases = new Cases
            {
                Active = dataAPI.Cases.Active,
                Critical = dataAPI.Cases.Critical,
                New = dataAPI.Cases.New,
                Recovered = dataAPI.Cases.Recovered,
                Total = dataAPI.Cases.Total
            };
            Deaths = new Deaths
            {
                New = dataAPI.Deaths.New,
                Total = dataAPI.Deaths.Total
            };
        Time = dataAPI.Time;
        }


        public CovidDataModel(CovidData dataDB)
        {
            Country = dataDB.Country;
            Cases = new Cases
            {
                Active = dataDB.CaseActive,
                Critical = dataDB.CaseCritical,
                New = dataDB.CaseNew,
                Recovered = dataDB.CaseRecovered,
                Total = dataDB.CaseTotal
            };
            Deaths = new Deaths
            {
                New = dataDB.DeathNew,
                Total = dataDB.DeathTotal
            };
            Time = DateTime.Parse(dataDB.Time);
        }





        public object Clone()
        {
            return new CovidDataModel
            {
                Cases = (Cases)this.Cases.Clone(),
                Deaths = (Deaths)this.Deaths.Clone(),

                Country = this.Country,
                Time = this.Time
            };
        }





    }

    public class CovidList<T> : List<T> where T : CovidDataModel
    {
        private int DataRatio { get; set; } = 1;

        public CovidList(T[] ItemList, bool isHistory = false) : base(ItemList.Reverse())
        {
            CommonInit(ItemList, isHistory);
        }

        public CovidList(CovidData[] ItemList, bool isHistory = false)
        {
            CommonInit((T[])ItemList.FromDBtoModelArray(), isHistory);
        }

        public CovidList(CovidDataAPI[] ItemList, bool isHistory = false)
        {
            CommonInit((T[])ItemList.FromAPItoModelArray(), isHistory);
        }




        private void CommonInit(T[] ItemList, bool isHistory = false)
        {
            if (isHistory)
            {
                int i = 0;
                while (i < this.Count - 1)
                {
                    if (this[i].Time.Date == this[i + 1].Time.Date)
                    {
                        if (this[i + 1].Cases.New == null)
                            this.RemoveAt(i + 1);
                        else
                            this.RemoveAt(i);
                    }
                    else
                        i++;
                }
            }
        }
            


        #region Dati Base

        public IList<double?> TotalCases()
        {
            List<double?> DoubleList = new List<double?>();

            for (int i = 0; i < this.Count; i++)
                if (i % DataRatio == 0)
                    if (this[i].Cases.Active != null)
                        DoubleList.Add(int.Parse(this[i].Cases.Active));
                    else
                        DoubleList.Add(DoubleList.Last());

            return DoubleList;
        }

        public IList<double?> TotalDeaths()
        {
            List<double?> DoubleList = new List<double?>();

            for (int i = 0; i < this.Count; i++)
                if (i % DataRatio == 0)
                    if (this[i].Deaths.Total != null)
                        DoubleList.Add(int.Parse(this[i].Deaths.Total));
                    else
                        DoubleList.Add(DoubleList.Last());

            return DoubleList;
        }

        public IList<double?> TotalRecoverd()
        {
            List<double?> DoubleList = new List<double?>();

            for (int i = 0; i < this.Count; i++)
                if (i % DataRatio == 0)
                    try
                    {
                        DoubleList.Add(int.Parse(this[i].Cases.Recovered));
                    }
                    catch
                    {
                        DoubleList.Add(DoubleList.Last());
                    }
            return DoubleList;
        }

        #endregion Dati Base

        #region Dati Elaborati

        public IList<double?> NewCases()
        {
            List<double?> DoubleList = new List<double?>();

            for (int i = 1; i < this.Count - 1; i++)
                if (i % DataRatio == 0)
                    if (this[i].Cases.New != null)
                        DoubleList.Add(int.Parse(this[i].Cases.New));
                    else
                        DoubleList.Add(null);

            return DoubleList;
        }

        public IList<double?> DiferenceIncrease()
        {
            List<double?> DoubleList = new List<double?>();

            for (int i = 0; i < this.Count - 2; i++)
                if (i % DataRatio == 0)
                {
                    if (this[i + 1].Cases.New == null || this[i].Cases.New == null)
                        DoubleList.Add(null);
                    else
                        DoubleList.Add(int.Parse(this[i + 1].Cases.New) - int.Parse(this[i].Cases.New));
                }

            return DoubleList;
        }

        #endregion Dati Elaborati

        public IList<string> ListTime()
        {
            List<string> labelList = new List<string>();

            for (int i = 0; i < this.Count; i++)
                if (i % DataRatio == 0)
                    labelList.Add(this[i].Time.Date.ToString("dd/MM/yyyy"));

            return labelList;
        }


    }
}


public static class CovidCoverterEX
{

    public static CovidDataModel FromAPItoModel(this CovidDataAPI dataAPI)
    {
        return new CovidDataModel
        {
            Country = dataAPI.Country,
            Cases = new Covid_World.SharedData.Models.Cases
            {
                Active = dataAPI.Cases.Active,
                Critical = dataAPI.Cases.Critical,
                New = dataAPI.Cases.New,
                Recovered = dataAPI.Cases.Recovered,
                Total = dataAPI.Cases.Total
            },
            Deaths = new Covid_World.SharedData.Models.Deaths
            {
                New = dataAPI.Deaths.New,
                Total = dataAPI.Deaths.Total
            },
            Time = dataAPI.Time
        };

    }

    public static CovidDataModel FromDBtoModel(this CovidData dataDB)
    {
        return new CovidDataModel
        {
            Country = dataDB.Country,
            Cases = new Covid_World.SharedData.Models.Cases
            {
                Active = dataDB.CaseActive,
                Critical = dataDB.CaseCritical,
                New = dataDB.CaseNew,
                Recovered = dataDB.CaseRecovered,
                Total = dataDB.CaseTotal
            },
            Deaths = new Covid_World.SharedData.Models.Deaths
            {
                New = dataDB.DeathNew,
                Total = dataDB.DeathTotal
            },
            Time = DateTime.Parse(dataDB.Time)
        };
    }
    

    public static CovidDataModel[] FromAPItoModelArray(this CovidDataAPI[] dataAPIs)
    {

        IList<CovidDataModel> CovidDataModel = new List<CovidDataModel>();
        foreach (var data in dataAPIs)
            CovidDataModel.Add(new CovidDataModel(data));

        return CovidDataModel.ToArray();
    }

    public static CovidDataModel[] FromDBtoModelArray( this CovidData[] coviddatas)
    {
        List<CovidDataModel> cd = new List<CovidDataModel>();

        IEnumerable<CovidData> enume = coviddatas.OrderByDescending(a => a.Time);

        foreach (CovidData cs in enume)
            cd.Add(new CovidDataModel(cs));


        return cd.ToArray();
    }


    public static IList<CovidData> MapModelToDBList<T>(this CovidList<T> covidList ) where T : CovidDataModel
    {
        IList<CovidData> TempCovidRow = new List<CovidData>();

        foreach (var CovidDataFromModels in covidList)
            TempCovidRow.Add(CovidDataFromModels.MapModelToDB());

        return TempCovidRow;

    }


    public static CovidData MapModelToDB( this CovidDataModel covidDataModel)
    {
        return new CovidData
        {
            Country = covidDataModel.Country,
            Time = covidDataModel.Time.ToString("yyyy/MM/dd H:mm:ss"),
            CaseNew = covidDataModel.Cases.New,
            CaseActive = covidDataModel.Cases.Active,
            CaseCritical = covidDataModel.Cases.Critical,
            CaseRecovered = covidDataModel.Cases.Recovered,
            CaseTotal = covidDataModel.Cases.Total,
            DeathNew = covidDataModel.Deaths.New,
            DeathTotal = covidDataModel.Deaths.Total
        };
    }




}


