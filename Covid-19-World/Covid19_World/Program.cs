using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Covid_World.EFDataAccessLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using Microsoft.Extensions.Configuration;
using Covid_World.SharedData.DB;
using Hangfire;
using Hangfire.MySql.Core;
using System.Data;

namespace Covid_World
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var cof = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(cof).CreateLogger();


            CreateHostBuilder(args).Build().BootStrapDB().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });





    }


    public static class ProgramEx
    {

        public static IHost BootStrapDB(this IHost host)
        {
            var logger = host.Services.GetService<ILoggerFactory>().CreateLogger<Program>();
            var Covid19WDbContext = host.Services.CreateScope().ServiceProvider.GetService<Covid19wDbContext>();
            var HangFireContext = host.Services.CreateScope().ServiceProvider.GetService<HangFireContext>();
            DatabaseUpdater databaseUpdater = (DatabaseUpdater)host.Services.GetService<IDatabaseUpdater>();
            logger.LogInformation("Validating status of Entity Framework migrations...");

            if (MigrateDatabase(Covid19WDbContext, logger))
            {
                try
                {
                    databaseUpdater.JobDelegate();
                }
                catch (Exception ex)
                {
                    logger.LogError("After the migration the database was not filled correctly with Message: {Message}", ex.Message);
                }
            }

            if (MigrateDatabase(HangFireContext, logger))
                logger.LogInformation("HangFire Migration Done");


            databaseUpdater.StartJob();


            logger.LogInformation("Validating status of Entity Framework migrations completed");

            return host;
        }

        /// <summary>
        /// applica le migrazioni del db
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="logger"></param>
        /// <returns>restituisce true se delle migrazioni sono state aplicate altrimenti false</returns>
        public static bool MigrateDatabase(DbContext dbContext, Microsoft.Extensions.Logging.ILogger logger)
        {

            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            var migrations = pendingMigrations as IList<string> ?? pendingMigrations.ToList();

            if (!migrations.Any())
            {
                logger.LogInformation("No pending migratons");

                return false;
            }
            else
            {
                logger.LogInformation("Pending migratons {MigratinCount} of {databaseName}", migrations.Count(), dbContext.Database.GetDbConnection().Database);

                foreach (var migration in migrations)
                    logger.LogInformation($"\t{migration}");

                logger.LogInformation("Applyting Entity Framework migrations");
                dbContext.Database.Migrate();
                logger.LogInformation("Migration completed");

                return true;
            }
        }

    }
}
