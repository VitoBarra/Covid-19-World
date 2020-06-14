using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid_World.Installers
{
    public static class InstallerExtension
    {

        public static void InstallServicesAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            List<IInstaller> Installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
            typeof(IInstaller).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();


            Installers.ForEach(installer => installer.InstallService(services,configuration));

        }

    }
}
