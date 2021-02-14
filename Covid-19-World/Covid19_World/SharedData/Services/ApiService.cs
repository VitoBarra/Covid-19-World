using Covid19_World.Shared.Models;
using Covid19_World.Shared.Services.Api.Model;
using Microsoft.Extensions.Logging;
using SharedLibrary.GeneralUse.RestService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Covid19_World.Shared.Services.Api
{
    public static class ApiService
    {
        public static bool Unstarted = true;

        //if any api need Header Add it Here
        public static void StartUpAPI()
        {
            if (Unstarted)
            {
                //HTTPClientFactory.DefHeaders.Add(new Header("Name", "value"));
                Unstarted = false;
            }
        }



        public static CovidDataAPI[] GetDataHistory(string Country = "all", ILogger logger = null)
        {
            
            CovidDataAPI[] Data = null;
            string EndPoint = $"https://api.covid19api.com/total/dayone/country/{Country}";


            try
            {
                Data = RestService.CallService<CovidDataAPI[]>(EndPoint);
            }
            catch (Exception ex)
            {
                if (logger != null)
                    logger.LogError("ERROR from GetDataHistory API Call: {Message}", ex.Message);
            }

            return Data;
        }
    }
}