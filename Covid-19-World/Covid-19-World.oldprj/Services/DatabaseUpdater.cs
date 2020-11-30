using Covid_World.EFDataAccessLibrary.DataAccess;
using Covid_World.SharedData.Models;
using Covid19_World.Shared.Models;
using Covid19_World.Shared.Services.Api;
using Covid19_World.Shared.Services.Api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Covid_World.SharedData.Models.CovidDataModel;

namespace Covid_World.SharedData.DB
{
    public class DatabaseUpdater : IHostedService, IDisposable
    {

        private Timer Timer;
        private Covid19wDbContext Covid19WDbContext;

        public IList<CountryPairs> CountryList { get; private set; }

        public DatabaseUpdater(Covid19wDbContext _db, IUtilityFileReader _contryShred)
        {
            Covid19WDbContext = _db;
            CountryList = ((UtilityFileReader)_contryShred).CountryList;
        }

        //This method runs at the start of the application
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Timer = new Timer( /*async*/ e =>
            {
                //IList<Task<IList<CovidDataAPI>>> CallList = new List<Task<IList<CovidDataAPI>>>();

                //foreach (CountryPairs s in CountryList)
                //    CallList.Add(ApiService.GetDataHistory(s.Country));


                //foreach (var i in await Task.WhenAll(CallList))
                //    new CovidList<CovidDataModel>(APIListTOModel(i.ToArray()), true).SaveOnDatabase(Covid19WDbContext);


                foreach (CountryPairs s in CountryList)
                    new CovidList<CovidDataModel>(ApiService.GetDataHistory(s.Country).ToArray(), true)
                    .SaveOnDatabase(Covid19WDbContext);

            }, null, TimeSpan.Zero, TimeSpan.FromHours(1));

            return Task.CompletedTask;
        }


        //--------shutdown operations---------//
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Timer?.Dispose();
        }



    }
}
