using System;
using System.ComponentModel.DataAnnotations;

namespace Covid_World.EFDataAccessLibrary.Models
{
    public partial class CovidData : ICloneable
    {
        [Required, DataType("int")]
        public int Id { get; set; }

        [Required]
        public string Country { get; set; }

        [Required, DataType("Time")]
        public string Time { get; set; }

        [MaxLength(45)]
        public string CaseNew { get; set; }

        [MaxLength(45)]
        public string CaseActive { get; set; }

        [MaxLength(45)]
        public string CaseRecovered { get; set; }

        [MaxLength(45)]
        public string CaseTotal { get; set; }

        [MaxLength(45)]
        public string DeathNew { get; set; }

        [MaxLength(45)]
        public string DeathTotal { get; set; }

        public CovidData() { }



        public object Clone()
        {
            return new CovidData
            {
                Id = this.Id,
                Country = this.Country,
                Time = this.Time,
                CaseNew = this.CaseNew,
                CaseActive = this.CaseActive,
                CaseRecovered = this.CaseRecovered,
                CaseTotal = this.CaseTotal,
                DeathNew = this.DeathNew,
                DeathTotal = this.DeathTotal
            };
        }
    }
}