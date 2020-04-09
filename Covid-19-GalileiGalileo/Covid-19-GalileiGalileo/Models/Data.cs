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

        public CovidList(T[] ItemList) : base(ItemList) { }

        public IList<double?> TotalCases()
        {
            List<double?> DoubleList = new List<double?>();
            foreach (CovidData cd in this)
            {
                DoubleList.Add(int.Parse(cd.Cases.Total));
            }
            return DoubleList;
        }


        public IList<double?> DiferenceCases()
        {
            List<double?> DoubleList = new List<double?>();
            for (int i = 0; i < this.Count - 1; i++)
            {
                DoubleList.Add(int.Parse(this[i].Cases.New) - int.Parse(this[i+1].Cases.New));
            }
            return DoubleList;
        }


        public IList<string> ListTime()
        {
            List<string> labelList = new List<string>();
            foreach (CovidData cd in this)
            {
                labelList.Add(cd.Time.ToString());
            }
            return labelList;
        }


    }


}

