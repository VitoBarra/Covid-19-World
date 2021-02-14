using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Covid19_World.Shared.Services.Api.Model
{



    public class CovidDataAPI
    {
        [JsonProperty("Country")]
        public string Country { get; set; }
        [JsonProperty("CountryCode")]
        public string CountryCode { get; set; }
        [JsonProperty("Province")]
        public string Province { get; set; }
        [JsonProperty("City")]
        public string City { get; set; }
        [JsonProperty("CityCode")]
        public string CityCode { get; set; }
        [JsonProperty("Lat")]
        public string Lat { get; set; }
        [JsonProperty("Lon")]
        public string Lon { get; set; }
        [JsonProperty("Confirmed")]
        public string Confirmed { get; set; }
        [JsonProperty("Deaths")]
        public string Deaths { get; set; }
        [JsonProperty("Recovered")]
        public string Recovered { get; set; }
        [JsonProperty("Active")]
        public string Active { get; set; }
        [JsonProperty("Date")]
        public DateTime Date { get; set; }
    }
}