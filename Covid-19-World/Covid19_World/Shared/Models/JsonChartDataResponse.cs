using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19_World.Shared.Models
{
    public class JsonChartDataResponse
    {
        [JsonProperty("ActiveCase")]
        public List<double?> ActiveCase { get; set; }
        [JsonProperty("TotalDeaths")]
        public List<double?> TotalDeaths { get; set; }
        [JsonProperty("TotalRecoverd")]
        public List<double?> TotalRecoverd { get; set; }


        [JsonProperty("DailyCases")]
        public List<double?> DailyCases { get; set; }

        [JsonProperty("DiferenceDailyCases")]
        public List<double?> DiferenceDailyCases { get; set; }
    }
}



                