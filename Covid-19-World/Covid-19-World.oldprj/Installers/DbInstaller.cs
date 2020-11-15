﻿using Covid_World.EFDataAccessLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using SharedLibrary.AspNetCore.Installers;
using System;

namespace Covid_World.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
#if DEBUG
            services.AddDbContext<Covid19wDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Covid19wSQLServer")));
#else
            services.AddDbContext<Covid19wDbContext>(options => options
            .UseMySql(configuration.GetConnectionString("Covid19wMariaDB"),
            mySqlOptions => mySqlOptions.ServerVersion(new Version(10, 5, 4), ServerType.MariaDb)));
#endif
        }
    }
}