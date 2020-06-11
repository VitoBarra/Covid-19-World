using ChartJSCore.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid_World.Models
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
}

