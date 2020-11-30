using Covid_World.EFDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;

namespace Covid_World.EFDataAccessLibrary.DataAccess
{
    public partial class Covid19wDbContext : DbContext
    {
        private string ConnectionString { get; set; }
        public Covid19wDbContext(DbContextOptions<Covid19wDbContext> options) : base(options)
        {
        }
        public Covid19wDbContext(DbContextOptions<Covid19wDbContext> options, string _ConnectionString)
        {
            ConnectionString = _ConnectionString;
        }

        public DbSet<CovidData> CovidDatas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#if DEBUG
                if (ConnectionString != null)
                    optionsBuilder.UseSqlServer(ConnectionString);
                
#else
                if (ConnectionString != null)
                {
                    optionsBuilder.UseMySql(ConnectionString,
                    mySqlOptions => mySqlOptions.ServerVersion(new Version(10, 5, 4), ServerType.MariaDb));
                }
#endif
                base.OnConfiguring(optionsBuilder);
            }
        }
    }
}