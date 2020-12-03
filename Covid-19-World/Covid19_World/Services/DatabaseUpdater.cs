using Covid_World.EFDataAccessLibrary.DataAccess;
using Covid_World.SharedData.Models;
using Covid19_World.Shared.Models;
using Covid19_World.Shared.Services.Api;
using Covid19_World.Shared.Services.Api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Covid_World.SharedData.Models.CovidDataModel;

namespace Covid_World.SharedData.DB
{
    public interface IDatabaseUpdater
    {
        void StartTimer();
        void TimerCall();
    }

    public class DatabaseUpdater : IDatabaseUpdater
    {

        private Timer Timer;
        private readonly Covid19wDbContext Covid19WDbContext;
        private readonly ILogger<DatabaseUpdater> _logger;
        public IList<CountryPairs> CountryList { get; private set; }

        public DatabaseUpdater(Covid19wDbContext _db, ILogger<DatabaseUpdater> logger, IUtilityFileReader _contryShred)
        {
            Covid19WDbContext = _db;
            CountryList = ((UtilityFileReader)_contryShred).CountryList;
            _logger = logger;

            StartTimer();
        }

        //This method runs at the start of the application
        public void StartTimer()
        {
            Timer = new Timer(e=>TimerCall(), null, TimeSpan.Zero, TimeSpan.FromHours(1));
        }

        public void TimerCall()
        {
            if (Covid19WDbContext.Database.GetPendingMigrations().Any()) 
            {
                _logger.LogDebug("Do the migration frist");
                return;
            }

                _logger.LogInformation("Filling Database...");
            try
            {
                foreach (CountryPairs s in CountryList)
                {
                    _logger.LogInformation("Request For ({Mapde}:{Country})", s.MapCode, s.Country);
                    new CovidList<CovidDataModel>(ApiService.GetDataHistory(s.Country, _logger).ToArray(), true).SaveOnDatabase(Covid19WDbContext);
                }
                _logger.LogInformation("Database filled successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Fill database failed with message: {Messasage}", ex.Message);
            }

            //IList<Task<IList<CovidDataAPI>>> CallList = new List<Task<IList<CovidDataAPI>>>();

            //foreach (CountryPairs s in CountryList)
            //    CallList.Add(ApiService.GetDataHistory(s.Country));


            //foreach (var i in await Task.WhenAll(CallList))
            //    new CovidList<CovidDataModel>(APIListTOModel(i.ToArray()), true).SaveOnDatabase(Covid19WDbContext);


        }



    }
}
