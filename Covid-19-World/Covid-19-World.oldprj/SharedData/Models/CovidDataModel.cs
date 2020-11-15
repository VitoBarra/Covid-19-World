using Covid_World.EFDataAccessLibrary.Models;
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

        public CovidData MapModelToDB()
        {
            return new CovidData
            {
                Country = Country,
                Time = Time.ToString("yyyy/MM/dd H:mm:ss"),
                CaseNew = Cases.New,
                CaseActive = Cases.Active,
                CaseCritical = Cases.Critical,
                CaseRecovered = Cases.Recovered,
                CaseTotal = Cases.Total,
                DeathNew = Deaths.New,
                DeathTotal = Deaths.Total
            };
        }
        public static CovidDataModel[] APIListTOModel(CovidDataAPI[] dataAPIs)
        {

            IList<CovidDataModel> CovidDataModel = new List<CovidDataModel>();
            foreach (var data in dataAPIs)
                CovidDataModel.Add(new CovidDataModel(data));

            return CovidDataModel.ToArray();
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

        public class CovidList<T> : List<T> where T : CovidDataModel
        {
            private int DataRatio { get; set; } = 1;

            public CovidList(T[] ItemList, bool isHistory = false) : base(ItemList.Reverse())
            {
                if (isHistory)
                {
                    //string str = "";
                    int i = 0;
                    while (i < this.Count - 1)
                    {
                        //str += $"{this[i].Time} A:{this[i].Cases.Active} N:{this[i].Cases.New} R:{this[i].Cases.Recovered} \n" ;
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
                        try
                        {
                            DoubleList.Add(int.Parse(this[i].Cases.Active));
                        }
                        catch
                        {
                            DoubleList.Add(DoubleList.Last());
                        }
                return DoubleList;
            }

            public IList<double?> TotalDeaths()
            {
                List<double?> DoubleList = new List<double?>();

                for (int i = 0; i < this.Count; i++)
                    if (i % DataRatio == 0)
                        try
                        {
                            DoubleList.Add(int.Parse(this[i].Deaths.Total));
                        }
                        catch
                        {
                            DoubleList.Add(DoubleList.Last());
                        }
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

            /// <summary>
            /// dati elaborati
            /// </summary>

            public IList<double?> NewCases()
            {
                List<double?> DoubleList = new List<double?>();

                for (int i = 1; i < this.Count - 1; i++)
                    if (i % DataRatio == 0)
                        try
                        {
                            DoubleList.Add(int.Parse(this[i].Cases.New));
                        }
                        catch
                        {
                            DoubleList.Add(null);
                        }
                return DoubleList;
            }

            public IList<double?> DiferenceIncrease()
            {
                List<double?> DoubleList = new List<double?>();

                for (int i = 0; i < this.Count - 2; i++)
                    if (i % DataRatio == 0)
                        try
                        {
                            DoubleList.Add(int.Parse(this[i + 1].Cases.New) - int.Parse(this[i].Cases.New));
                        }
                        catch
                        {
                            if (this[i + 1].Cases.New == null || this[i].Cases.New == null)
                                DoubleList.Add(null);
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



            public IList<CovidData> MapModelToDBList()
            {
                IList<CovidData> TempCovidRow = new List<CovidData>();

                foreach (var CovidDataFromModels in this)
                    TempCovidRow.Add(CovidDataFromModels.MapModelToDB());

                return TempCovidRow;

            }
        }
    }
}
