using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Covid19_World.Shared.Services.Api.Model
{
    public class CovidDataAPI
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

    
}