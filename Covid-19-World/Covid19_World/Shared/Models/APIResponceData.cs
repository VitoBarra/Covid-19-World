using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19_World.Shared.Models
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
        public List<CovidDataAPI> Response { get; set; } = new List<CovidDataAPI>();
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
        public List<CovidDataAPI> Response { get; set; } = new List<CovidDataAPI>();
    }


    public class Parameters
    {
        [JsonProperty("country")]
        public string Country { get; set; }
    }

    public class CountryPairs
    {
        [JsonProperty("Country")]
        public string Country { get; set; }
        [JsonProperty("MapCode")]
        public string MapCode { get; set; }
    }
}

