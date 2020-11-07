using Covid_World.Services;
using Covid19_World.Shared.Models;
using Newtonsoft.Json;
using SharedLibrary.GeneralUse.RestService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SharedLibrary.GeneralUse.RestService;

namespace Covid_World.Services
{
    public static class RestServices
    {
        public static bool Unstarted = true;

        public static void StartUpAPI()
        {
            if (Unstarted)
            {
                RestService.client.DefaultRequestHeaders.Add("x-rapidapi-host", "covid-193.p.rapidapi.com");
                RestService.client.DefaultRequestHeaders.Add("x-rapidapi-key", "f4d025568cmsh12e79fdec8e33b1p174ff5jsn48a341aa1fc0");
                Unstarted = false;
            }

        }

        public async static Task<IList<CovidDataAPI>> GetDataHistory(string Country ="all")
        {

            IList<CovidDataAPI> Data = null;
            string EndPoint = $"https://covid-193.p.rapidapi.com/history?country={Country}";

            try
            {
                 Data = (await RestService.CallServiceAsync<ResponceHistory>(EndPoint)).Response;
            }
            catch (Exception ex)
            {
                using (System.IO.StreamWriter file =new System.IO.StreamWriter(@".\APIErrorLog.txt", true))
                {
                    file.WriteLine($"ERROR from GetDataHistory: {ex.Message}" );
                }
            }

            return Data;
        }




        public async static Task<IList<CovidDataAPI>> GetStatByCountry(string country = null)
        {
            IList<CovidDataAPI> Data = null;
            string EndPoint;
            if (string.IsNullOrEmpty(country))
                EndPoint = $"https://covid-193.p.rapidapi.com/statistics";
            else
                EndPoint = $"https://covid-193.p.rapidapi.com/statistics?country={country}";

            try
            {
                Data = (await RestService.CallServiceAsync<Responce>(EndPoint)).Response;
            }
            catch (Exception ex)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@".\APIErrorLog.txt", true))
                {
                    file.WriteLine($"ERROR from GetStatByCountry: {ex.Message}");
                }
            }

            return Data;
        }

        public async static Task<IList<string>> GetCountryList()
        {
            IList<string> Data = null;
            string EndPoint = $"https://covid-193.p.rapidapi.com/countries";

            try
            {
                Data = (await RestService.CallServiceAsync<List<string>>(EndPoint));
            }
            catch (Exception ex)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@".\APIErrorLog.txt", true))
                {
                    file.WriteLine($"ERROR from GetCountryList: {ex.Message}");
                }
            }

            return Data;
        }




    }
}
