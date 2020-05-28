using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid_19_GalileiGalileo.Models
{
    public class JsonChartDataResponse
    {
        [JsonProperty("ActiveCase")]
        public List<double?> ActiveCase { get; set; }
        [JsonProperty("TotalDeaths")]
        public List<double?> TotalRecoverd { get; set; }
        [JsonProperty("TotalRecoverd")]
        public List<double?> DailyCases { get; set; }


        [JsonProperty("DailyCases")]
        public List<double?> TotalDeaths { get; set; }

        [JsonProperty("DiferenceDailyCases")]
        public List<double?> DiferenceDailyCases { get; set; }
    }
}
