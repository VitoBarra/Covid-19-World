using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Covid19_World.Shared.Services.Api.Model
{
    /// <summary>
    /// a class that contains the meta data of the country also used in views so be careful when you change it
    /// </summary>
    public class CountryMetaData
    {
        //to user in app
        [JsonProperty("Country")]
        public string Country { get; set; }
        //to call api
        [JsonProperty("Slug")]
        public string Slug { get; set; }
        [JsonProperty("ISO2")]
        public string ISO2 { get; set; }
    }
}