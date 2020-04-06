using ChartJSCore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid_19_GalileiGalileo.Models
{
    
    public class CovidData
    {
        [JsonProperty("total_deaths")]
        public string total_deaths { get; set; }
        [JsonProperty("total_cases")]
        public string total_cases { get; set; }
        [JsonProperty("total_recovered")]
        public string total_recovered { get; set; }
        [JsonProperty("new_cases")]
        public string new_cases { get; set; }
        [JsonProperty("new_deaths")]
        public string new_deaths { get; set; }
        public List<Dataset> Datasets { get; internal set; }
        [JsonProperty("statistic_taken_at")]
        DateTime statistic_taken_at { get; set; }
    }
}
