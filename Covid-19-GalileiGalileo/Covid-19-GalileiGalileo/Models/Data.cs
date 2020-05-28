using ChartJSCore.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid_19_GalileiGalileo.Models
{


    public class Responce
    {
        [JsonProperty("get")]
        public string Get { get; set; }
        [JsonProperty("parameters")]
        public Parameters[] Parameters { get; set; }
        [JsonProperty("errors")]
        public string[] errors { get; set; }
        [JsonProperty("results")]
        public string Results { get; set; }
        [JsonProperty("response")]
        public List<CovidData> Response { get; set; } = new List<CovidData>();
    }

    public class ResponceHistory
    {
        [JsonProperty("get")]
        public string Get { get; set; }
        [JsonProperty("parameters")]
        public Parameters Parameters { get; set; }
        [JsonProperty("errors")]
        public string[] errors { get; set; }
        [JsonProperty("results")]
        public string Results { get; set; }
        [JsonProperty("response")]
        public List<CovidData> Response { get; set; } = new List<CovidData>();
    }


    public class Parameters
    {
        [JsonProperty("country")]
        public string Country { get; set; }
    }


    public class CovidData
    {
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("cases")]
        public Cases Cases { get; set; }
        [JsonProperty("deaths")]
        public Deaths Deaths { get; set; }
        [JsonProperty("time")]
        public DateTime Time { get; set; }


    }

    public class Cases
    {
        [JsonProperty("new")]
        public string New { get; set; }
        [JsonProperty("active")]
        public string Active { get; set; }
        [JsonProperty("critical")]
        public string Critical { get; set; }
        [JsonProperty("recovered")]
        public string Recovered { get; set; }
        [JsonProperty("total")]
        public string Total { get; set; }
    }


    public class Deaths
    {
        [JsonProperty("new")]
        public string New { get; set; }
        [JsonProperty("total")]
        public string Total { get; set; }
    }






    public class CovidList<T> : List<T> where T : CovidData
    {
        int DataRatio { get; set; } = 1;

        public CovidList(T[] ItemList, bool isHistory = false) : base(ItemList.Reverse())
        {
            if (isHistory)
            {
                int i = 0;
                while (i != this.Count - 1)
                {
                    if (this[i].Time.Day == this[i + 1].Time.Day)
                        this.RemoveAt(i);
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


        #endregion


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
                        DoubleList.Add(0);
                    }
            return DoubleList;
        }

        public IList<double?> DiferenceIncrease()
        {
            List<double?> DoubleList = new List<double?>();

            for (int i = 1; i < this.Count - 1; i++)
                if (i % DataRatio == 0)
                    try
                    {
                        DoubleList.Add(int.Parse(this[i + 1].Cases.New) - int.Parse(this[i].Cases.New));
                    }
                    catch
                    {
                        if (this[i + 1].Cases.New == null)
                            DoubleList.Add(-int.Parse(this[i].Cases.New));
                        else if (this[i ].Cases.New == null)
                            DoubleList.Add(int.Parse(this[i + 1].Cases.New));
                        else
                            DoubleList.Add(0);
                    }

            return DoubleList;
        }
        #endregion


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

