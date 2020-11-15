using Covid_World.EFDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;

namespace Covid_World.EFDataAccessLibrary.DataAccess
{
    public partial class Covid19wDbContext : DbContext
    {
        public Covid19wDbContext(DbContextOptions<Covid19wDbContext> options) : base(options)
        {
        }

        public DbSet<CovidData> CovidDatas { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#if DEBUG
                optionsBuilder.UseSqlServer(@"server=127.0.0.1;uid=root;pwd=toor;database=covid19w");
#else
                optionsBuilder.UseMySql(@"server=localhost;user id=root;password=toor;database=covid19w",
                mySqlOptions => mySqlOptions.ServerVersion(new Version(10, 5, 4), ServerType.MariaDb));
#endif
            }
        }
    }
}