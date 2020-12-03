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

namespace Covid_World
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var cof = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(cof).CreateLogger();


            CreateHostBuilder(args).Build().DoMigration().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

    }

    public static class ProgramEX
    {
        public static IHost DoMigration(this IHost host)
        {
            var logger = host.Services.GetService<ILoggerFactory>().CreateLogger<Program>();
            var CovidContext = host.Services.CreateScope().ServiceProvider.GetService<Covid19wDbContext>();
            DatabaseUpdater databaseUpdater = (DatabaseUpdater)host.Services.GetService<IDatabaseUpdater>();


            logger.LogInformation("Validating status of Entity Framework migrations...");
            var pendingMigrations = CovidContext.Database.GetPendingMigrations();
            var migrations = pendingMigrations as IList<string> ?? pendingMigrations.ToList();
            if (!migrations.Any())
                logger.LogInformation("No pending migratons");
            else
            {
                logger.LogInformation("Pending migratons {MigratinCount}", migrations.Count());

                foreach (var migration in migrations)
                    logger.LogInformation($"\t{migration}");

                logger.LogInformation("Applyting Entity Framework migrations");
                CovidContext.Database.Migrate();
                logger.LogInformation("Migration completed");

                try
                {
                    databaseUpdater.TimerCall();
                }
                catch (Exception ex)
                {
                    logger.LogError("After the migration the database was not filled correctly with Message: {Message}", ex.Message);
                }
            }
            logger.LogInformation("Validating status of Entity Framework migrations completed");

            return host;
        }
    }
}