using Covid_World.EFDataAccessLibrary.DataAccess;
using Covid_World.SharedData.Models;
using Covid19_World.Shared.Models;
using Covid19_World.Shared.Services.Api;
using Covid19_World.Shared.Services.Api.Model;
using Hangfire;
using Hangfire.MySql.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Covid_World.SharedData.Models.CovidDataModel;

namespace Covid_World.SharedData.DB
{
    public interface IDatabaseUpdater
    {
        void StartJob();
        void JobDelegate();

    }

    public class DatabaseUpdater : IDatabaseUpdater
    {
        //dependesy injected
        private readonly Covid19wDbContext Covid19WDbContext;
        private readonly HangFireContext HangFireContext;
        private readonly ILogger<DatabaseUpdater> logger;

        private readonly RetryPolicy ApiCallRetryPolicy;

        public IList<CountryMetaData> CountryList { get; private set; }

        public DatabaseUpdater(Covid19wDbContext _db,HangFireContext _hf, ILogger<DatabaseUpdater> _logger, IUtilityFileReader _contryShred)
        {
            Covid19WDbContext = _db;
            HangFireContext = _hf;
            CountryList = ((UtilityFileReader)_contryShred).CountryList;
            logger = _logger;

            ApiCallRetryPolicy = Policy.Handle<Exception>()
                        .Retry(3, (ex, RetryCount) => this.logger.LogInformation("Api call Failed with message: {Message}\n retried {RetryCount} Time", ex.Message, RetryCount));

        }

        //This method runs at the start of the application
        public void StartJob()
        {
            if (!HangFireContext.Database.GetAppliedMigrations().Any())
            {
                RecurringJob.AddOrUpdate(() => JobDelegate(), cronExpression: "0 0 0/3 ? * * *");
            }
        }

        public void JobDelegate()
        {
            if (Covid19WDbContext.Database.GetPendingMigrations().Any())
            {
                logger.LogDebug("Do the migration frist");
                return;
            }

            logger.LogInformation("Filling Database...");

            foreach (CountryMetaData s in CountryList)
            {
                logger.LogInformation("Request For ( {Mapde} ~||~ {Slug} ~||~ {Country})", s.ISO2, s.Slug, s.Country);

                try
                {
                        ApiCallRetryPolicy.Execute(() =>
                        new CovidList<CovidDataModel>(ApiService.GetDataHistory(s.Slug, logger), true)
                            .SaveOnDatabase(Covid19WDbContext));
                }
                catch (Exception ex)
                {
                    logger.LogError("Fill database failed with message: {Messasage}", ex.Message);
                }
            }


            try
            {
                Covid19WDbContext.GenerateSumOfAll().SaveOnDatabase(Covid19WDbContext);
            }
            catch (Exception ex)
            {
                logger.LogError("Fill database failed on Generate Total with message: {Messasage}", ex.Message);
            }
            logger.LogInformation("Database filled successfully");

            //IList<Task<IList<CovidDataAPI>>> CallList = new List<Task<IList<CovidDataAPI>>>();

            //foreach (CountryPairs s in CountryList)
            //    CallList.Add(ApiService.GetDataHistory(s.Country));


            //foreach (var i in await Task.WhenAll(CallList))
            //    new CovidList<CovidDataModel>(APIListTOModel(i.ToArray()), true).SaveOnDatabase(Covid19WDbContext);


        }


    }
}
