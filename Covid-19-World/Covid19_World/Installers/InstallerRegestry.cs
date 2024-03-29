﻿using Covid_World.EFDataAccessLibrary.DataAccess;
using Covid_World.SharedData.DB;
using Covid_World.SharedData.Models;
using Covid19_World.Shared.Services.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using SharedLibrary.AspNetCore.Installers;
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Hangfire;
using Hangfire.MySql;
using Hangfire.MySql.Core;
using System.Data;

namespace Covid_World.Installers
{
    public class InstallerRegestry : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            //MVCinstallers
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //UtilityFileReader
            services.AddSingleton<IUtilityFileReader, UtilityFileReader>();





            //Database Installer
#if DEBUG
            string Connection = configuration.GetConnectionString("Covid19wSQLServer");
            string ConnectionHangFire = configuration.GetConnectionString("HangFireSQLServer");
            services.AddDbContext<Covid19wDbContext>(options => options.UseSqlServer(Connection));
            services.AddDbContext<HangFireContext>(options => options.UseSqlServer(ConnectionHangFire));
#else
            var Host = configuration["DB_NAME"] ?? "localhost";
            var user = configuration["DB_USERNAME"] ?? "root";
            var password = configuration["DB_PASSWORD"] ?? "toor";
            var scheme = configuration["DB_SCHEME_NAME"] ?? "covid19w";


            string Connection = $"server = {Host}; port = 3306; userid = {user}; password = {password};";
            string ConnectionCovid = Connection + $"database = {scheme};";
            string ConnectionHangFire = Connection + $"database = HangFire;";


            //services.AddDbContext<Covid19wDbContext>(options => options.UseMySQL(Connection));
            //services.AddDbContext<Covid19wDbContext>(options => options.UseMySql(new MariaDbServerVersion(new Version(10, 5))));
            services.AddDbContext<Covid19wDbContext>(options => options.UseMySql(ConnectionCovid, mySqlOptions =>
            {
                mySqlOptions.ServerVersion(new Version(10, 5), ServerType.MariaDb);
                mySqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
            }));
            services.AddDbContext<HangFireContext>(options => options.UseMySql(ConnectionHangFire, mySqlOptions =>
            {
                mySqlOptions.ServerVersion(new Version(10, 5), ServerType.MariaDb);
                mySqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
            }));
#endif


            //DatabaseUpdater Service
            services.AddSingleton<IDatabaseUpdater, DatabaseUpdater>(p =>
            new DatabaseUpdater(
            p.CreateScope().ServiceProvider.GetService<Covid19wDbContext>(),
            p.CreateScope().ServiceProvider.GetService<HangFireContext>(),
            p.GetService<ILoggerFactory>().CreateLogger<DatabaseUpdater>(),
            p.GetService<IUtilityFileReader>()
            ));



            //Hangfire services
            services.AddHangfire(conf=>conf   
           .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
           .UseSimpleAssemblyNameTypeSerializer()
           .UseRecommendedSerializerSettings()
           .UseStorage(new MySqlStorage(ConnectionHangFire,
               new MySqlStorageOptions
               {
                   TransactionIsolationLevel = IsolationLevel.ReadCommitted,
                   QueuePollInterval = TimeSpan.FromSeconds(15),
                   JobExpirationCheckInterval = TimeSpan.FromHours(1),
                   CountersAggregateInterval = TimeSpan.FromMinutes(5),
                   PrepareSchemaIfNecessary = true,
                   DashboardJobListLimit = 50000,
                   TransactionTimeout = TimeSpan.FromMinutes(1),
               })));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

        }
    }
}