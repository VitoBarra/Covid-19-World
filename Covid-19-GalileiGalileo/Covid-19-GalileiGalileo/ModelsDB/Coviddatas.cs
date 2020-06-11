using System;
using System.Collections.Generic;

namespace Covid_World.ModelsDB
{
    public partial class Coviddatas
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Time { get; set; }
        public string CaseNew { get; set; }
        public string CaseActive { get; set; }
        public string CaseCritical { get; set; }
        public string CaseRecovered { get; set; }
        public string CaseTotal { get; set; }
        public string DeathNew { get; set; }
        public string DeathTotal { get; set; }
    }
}
