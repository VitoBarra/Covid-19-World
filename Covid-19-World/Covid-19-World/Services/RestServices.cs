﻿using Covid_World.Models;
using Covid_World.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Covid_World.Services
{
    public static class RestServices
    {
        public static HttpClient client = new HttpClient();
        public static bool Unstarted = true;

        public static void StartUpAPI()
        {
            if (Unstarted)
            {
                client.DefaultRequestHeaders.Add("x-rapidapi-host", "covid-193.p.rapidapi.com");
                client.DefaultRequestHeaders.Add("x-rapidapi-key", "f4d025568cmsh12e79fdec8e33b1p174ff5jsn48a341aa1fc0");
                Unstarted = false;
            }

        }

        public static IList<CovidData> GetDataHistory(out HttpResponseMessage response, string Country ="all")
        {
            IList<CovidData> Data = null;
            string EndPoint = $"https://covid-193.p.rapidapi.com/history?country={Country}";

            try
            {
                response = client.GetAsync(EndPoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    Data = JsonConvert.DeserializeObject<ResponceHistory>(content).Response;
                }
            }
            catch (Exception ex)
            {
                using (System.IO.StreamWriter file =new System.IO.StreamWriter(@".\APIErrorLog.txt", true))
                {
                    file.WriteLine($"ERROR from GetDataHistory: {ex.Message}" );
                }
                response = null;
            }

            return Data;
        }


        public static IList<CovidData> GetStatByCountry(out HttpResponseMessage response,string country = null)
        {
            IList<CovidData> Data = null;
            string EndPoint;
            if (string.IsNullOrEmpty(country))
                EndPoint = $"https://covid-193.p.rapidapi.com/statistics";
            else
                EndPoint = $"https://covid-193.p.rapidapi.com/statistics?country={country}";

            try
            {
                response = client.GetAsync(EndPoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    Data = JsonConvert.DeserializeObject<Responce>(content).Response;
                }
            }
            catch (Exception ex)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@".\APIErrorLog.txt", true))
                {
                    file.WriteLine($"ERROR from GetStatByCountry: {ex.Message}");
                }
                response = null;
            }

            return Data;
        }

        public static IList<string> GetCountryList()
        {
            IList<string> Data = null;
            string EndPoint = $"https://covid-193.p.rapidapi.com/countries";

            try
            {
                HttpResponseMessage response = client.GetAsync(EndPoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    Data = JsonConvert.DeserializeObject<List<string>>(content);
                }
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