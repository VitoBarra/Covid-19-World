using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid_19_GalileiGalileo.Seriveces;
using Covid_19_GalileiGalileo.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Covid_19_GalileiGalileo
{
    public class Program
    {
        static CovidDataData general;
        public static void Main(string[] args)
        {
            RestServices.StartUpAPI();
            getData();
            CreateHostBuilder(args).Build().Run();
        }

        public static async void getData()
        {
            general = await RestServices.GetDataWorld();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
