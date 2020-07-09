using System;
using Covid_World.EFDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Covid_World.EFDataAccessLibrary.DataAccess
{
    public partial class Covid19wDbContext : DbContext
    {
  
        public Covid19wDbContext( DbContextOptions<Covid19wDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Coviddatas> Coviddatas { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#if DEBUG
                optionsBuilder.UseMySQL("server=localhost;user id=root;password=toor;persistsecurityinfo=True;database=Covid19w;");
#else
                optionsBuilder.UseSqlServer("Server=db834093830.hosting-data.io;Database=db834093830;User Id=dbo834093830;Password=RSai@fHz6y&9;");
#endif
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coviddatas>(entity =>
            {
                entity.ToTable("coviddatas");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CaseActive)
                    .HasColumnName("Case_Active")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.CaseCritical)
                    .HasColumnName("Case_Critical")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.CaseNew)
                    .HasColumnName("Case_new")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.CaseRecovered)
                    .HasColumnName("Case_Recovered")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.CaseTotal)
                    .HasColumnName("Case_Total")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.DeathNew)
                    .HasColumnName("Death_new")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.DeathTotal)
                    .HasColumnName("Death_Total")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Time)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Cognome)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
