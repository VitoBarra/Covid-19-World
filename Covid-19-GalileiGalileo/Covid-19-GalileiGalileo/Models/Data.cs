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

        public CovidList(T[] ItemList) : base(ItemList) 
        {
            int i = 0;
            while (i != this.Count-1)
            {
                if (this[i].Time.Day == this[i+1].Time.Day)
                    this.RemoveAt(i + 1);
                else
                    i++;
            }
        }

        public IList<double?> TotalCases(int DataRatio = 1)
        {
            List<double?> DoubleList = new List<double?>();

            for (int i = 0; i < this.Count; i++)
                if (i % DataRatio == 0)
                    DoubleList.Add(int.Parse(this[i].Cases.Active));

            return DoubleList;
        }


        public IList<double?> DiferenceIncrease(int DataRatio = 1)
        {
            List<double?> DoubleList = new List<double?>();

            for (int i = 1; i < this.Count - 1; i++)
                if (i % DataRatio == 0)
                    DoubleList.Add(int.Parse(this[i].Cases.New) - int.Parse(this[i + 1].Cases.New));

            return DoubleList;
        }

        public IList<double?> NewCases(int DataRatio = 1)
        {
            List<double?> DoubleList = new List<double?>();

            for (int i = 1; i < this.Count - 1; i++)
                if (i % DataRatio == 0)
                    DoubleList.Add(int.Parse(this[i].Cases.New));

            return DoubleList;
        }
        public IList<double?> fixeds(int DataRatio = 1)
        {
            List<double?> DoubleList = new List<double?>();

            for (int i = 1; i < this.Count - 1; i++)
                if (i % DataRatio == 0)
                    DoubleList.Add(int.Parse(this[i].Cases.Active) - int.Parse(this[i + 1].Cases.Active) + 
                        (int.Parse(this[i].Cases.Recovered)-int.Parse(this[i+1].Cases.Recovered)));

            return DoubleList;
        }

        //tot = OGcasiAt +OGcasMort+OgCasRec

        // casi attivi = casi attivi ieri + casi nuovi oggi -morti oggi - guariti oggi

            //nuovi attivi oggi = casi nuovi-casi morti oggi -casi guariti

        public IList<string> ListTime(int DataRatio = 1)
        {
            List<string> labelList = new List<string>();

            for (int i = 0; i < this.Count; i++)
                if (i % DataRatio == 0)
                    labelList.Add(this[i].Time.Date.ToString("dd/MM/yyyy"));

            return labelList;
        }




    }


}

