using System;
using Covid_World.EFDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Covid_World.EFDataAccessLibrary.DataAccess
{
    public partial class Covid19wDbContext : DbContext
    {
  
        public Covid19wDbContext( DbContextOptions<Covid19wDbContext> options) : base(options){}

        public DbSet<CovidDatas> CovidDatas { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#if DEBUG
                optionsBuilder.UseSqlServer(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Covid19World;Integrated Security=True");
#else
                optionsBuilder.UseSqlServer(@"Server=db834093830.hosting-data.io;Database=db834093830;User Id=dbo834093830;Password=RSai@fHz6y&9;");
#endif
            }
        }
    }
}
