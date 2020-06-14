using Covid_World.ModelsDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddDbContext<Covid19wDbContext>(options => options.UseMySQL(configuration.GetConnectionString("Default")));
#else
                     
            services.AddDbContext<Covid19wDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("Covid19wDB")));       
#endif
        }
    }
}
