using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Covid_World.EFDataAccessLibrary.Models
{
    public partial class CovidDatas
    {
        [Required, DataType("int")]
        public int Id { get; set; }
        [Required]
        public string Country { get; set; }
        [Required,DataType("Time")]
        public string Time { get; set; }
        [MaxLength(45)]
        public string CaseNew { get; set; }
        [MaxLength(45)]
        public string CaseActive { get; set; }
        [MaxLength(45)]
        public string CaseCritical { get; set; }
        [MaxLength(45)]
        public string CaseRecovered { get; set; }
        [MaxLength(45)]
        public string CaseTotal { get; set; }
        [MaxLength(45)]
        public string DeathNew { get; set; }
        [MaxLength(45)]
        public string DeathTotal { get; set; }
    }
}
