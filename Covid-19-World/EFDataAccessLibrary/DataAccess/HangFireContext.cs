using Covid_World.EFDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covid_World.EFDataAccessLibrary.DataAccess
{
    public class HangFireContext : DbContext
    {
        private string ConnectionString { get; set; }
        public HangFireContext(DbContextOptions<HangFireContext> options) : base(options)
        {
        }
        public HangFireContext(DbContextOptions<HangFireContext> options, string _ConnectionString)
        {
            ConnectionString = _ConnectionString;
        }

        //public DbSet<CovidData> CovidDatas { get; set; }

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
