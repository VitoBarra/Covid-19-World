using Newtonsoft.Json;
using System.Collections.Generic;

namespace Covid19_World.Shared.Models
{
    public class JsonChartDataResponse
    {
        [JsonProperty("ActiveCase")]
        public IList<double?> ActiveCase { get; set; }

        [JsonProperty("TotalDeaths")]
        public IList<double?> TotalDeaths { get; set; }

        [JsonProperty("TotalRecoverd")]
        public IList<double?> TotalRecoverd { get; set; }

        [JsonProperty("DailyCases")]
        public IList<double?> DailyCases { get; set; }

        [JsonProperty("DiferenceDailyCases")]
        public IList<double?> DiferenceDailyCases { get; set; }
        [JsonProperty("LabelList")]
        public IList<string> LabelList { get; set; }
    }
}