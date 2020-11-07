using Covid_World.EFDataAccessLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.AspNetCore.Installers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid_World.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
#if DEBUG
            services.AddDbContext<Covid19wDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
#else       
            services.AddDbContext<Covid19wDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Covid19wDB")));       
#endif
        }
    }
}
