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

namespace Covid_World
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var commandLineApplication = new CommandLineApplication(false);
            var doMigrate = commandLineApplication.Option(
                "--ef-migrate",
                "Apply entity framework migrations and exit",
                CommandOptionType.NoValue);
            var verifyMigrate = commandLineApplication.Option(
                "--ef-migrate-check",
                "Check the status of entity framework migrations",
                CommandOptionType.NoValue);
            commandLineApplication.HelpOption("-? | -h | --help");
            commandLineApplication.OnExecute(() =>
            {
                ExecuteApp(args, doMigrate, verifyMigrate);
                return 0;
            });
            commandLineApplication.Execute(args);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging((c, l) =>
            {
                l.ClearProviders();
                l.AddConfiguration(c.Configuration.GetSection("Logging"));
                l.AddDebug();
                l.AddConsole();

            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });



        private static void ExecuteApp(string[] args, CommandOption doMigrate, CommandOption verifyMigrate)
        {
            Console.WriteLine("Loading web host");

            var webHost = CreateHostBuilder(args).Build();

            if (verifyMigrate.HasValue() && doMigrate.HasValue())
            {
                Console.WriteLine("ef-migrate and ef-migrate-check are mutually exclusive, select one, and try again");
                Environment.Exit(2);
            }

            if (verifyMigrate.HasValue())
            {
                Console.WriteLine("Validating status of Entity Framework migrations");
                using (var serviceScope = webHost.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetService<Covid19wDbContext>())
                    {
                        var pendingMigrations = context.Database.GetPendingMigrations();
                        var migrations = pendingMigrations as IList<string> ?? pendingMigrations.ToList();
                        if (!migrations.Any())
                        {
                            Console.WriteLine("No pending migratons");
                            Environment.Exit(0);
                        }

                        Console.WriteLine("Pending migratons {0}", migrations.Count());
                        foreach (var migration in migrations)
                        {
                            Console.WriteLine($"\t{migration}");
                        }

                        Environment.Exit(3);
                    }
                }
            }

            if (doMigrate.HasValue())
            {
                Console.WriteLine("Applyting Entity Framework migrations");
                using (var serviceScope = webHost.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetService<Covid19wDbContext>())
                    {
                        context.Database.Migrate();
                        Console.WriteLine("All done, closing app");
                        Environment.Exit(0);
                    }
                }
            }

            // no flags provided, so just run the webhost
            webHost.Run();
        }


    }
}