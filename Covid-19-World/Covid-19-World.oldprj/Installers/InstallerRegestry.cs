using Covid_World.EFDataAccessLibrary.DataAccess;
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
            services.AddDbContext<Covid19wDbContext>(options => options.UseSqlServer(Connection));
#else
            string Connection = configuration.GetConnectionString("Covid19wMariaDB");
            //services.AddDbContext<Covid19wDbContext>(options => options.UseMySQL(Connection));
            //services.AddDbContext<Covid19wDbContext>(options => options.UseMySql(new MariaDbServerVersion(new Version(10, 5, 4))));
            services.AddDbContext<Covid19wDbContext>(options => options.UseMySql(Connection, mySqlOptions => mySqlOptions.ServerVersion(new Version(10, 5, 4), ServerType.MariaDb)));
#endif
            //DatabaseUpdater
            services.AddSingleton<IHostedService, DatabaseUpdater>(p=>
            new DatabaseUpdater(new Covid19wDbContext(null, Connection),
            p.GetService<IUtilityFileReader>()));


        }
    }
}