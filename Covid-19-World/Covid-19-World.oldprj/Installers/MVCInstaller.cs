using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.AspNetCore.Installers;

namespace Covid_World.Installers
{
    public class MVCInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }
    }
}