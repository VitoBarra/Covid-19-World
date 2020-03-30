using Covid_19_GalileiGalileo.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Covid_19_GalileiGalileo.Seriveces
{
    public static class RestServices
    {
        public static HttpClient client = new HttpClient();

        public static void StartUpAPI()
        {
            client.DefaultRequestHeaders.Add("x-rapidapi-host", "coronavirus-monitor.p.rapidapi.com");
            client.DefaultRequestHeaders.Add("x-rapidapi-key", "f4d025568cmsh12e79fdec8e33b1p174ff5jsn48a341aa1fc0");
        }


        public static async Task<CovidDataData> GetDataWorld()
        {
            CovidDataData Data = null;
            string EndPoint = $"https://coronavirus-monitor.p.rapidapi.com/coronavirus/worldstat.php";

            try
            {
                HttpResponseMessage response = await client.GetAsync(EndPoint);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<CovidDataData>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return Data;
        }


        public static async Task<CovidDataData> GetLatestStatByCountry(string CountryName)
        {

            CovidDataData Data = null;
            string EndPoint = $"https://coronavirus-monitor.p.rapidapi.com/coronavirus/latest_stat_by_country.php?country=${CountryName}";

            try
            {
                HttpResponseMessage response = await client.GetAsync(EndPoint);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<CovidDataData>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return Data;
        }



        public static async Task<CovidDataData> GetDataHistoryByCountry(string CountryName)
        {
            CovidDataData Data = null;
            string EndPoint = $"https://coronavirus-monitor.p.rapidapi.com/coronavirus/cases_by_particular_country.php?country={CountryName}";

            try
            {
                HttpResponseMessage response = await client.GetAsync(EndPoint);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<CovidDataData>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return Data;
        }





    }
}
