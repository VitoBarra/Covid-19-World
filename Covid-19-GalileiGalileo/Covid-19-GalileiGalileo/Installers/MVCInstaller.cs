using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Westwind.AspNetCore.LiveReload;

namespace Covid_World.Installers
{
    public class MVCInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {


#if DEBUG
            services.AddLiveReload();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
#else
                   services.AddControllersWithViews();    
#endif
        }
    }
}
