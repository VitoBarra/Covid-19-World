using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Covid_World.EFDataAccessLibrary.Models
{
    public partial class User
    {
        [Required, DataType("int")]
        public int Id { get; set; }
        [MaxLength(45)]
        public string Email { get; set; }
        [MaxLength(45)]
        public string Nome { get; set; }
        [MaxLength(45)]
        public string Cognome { get; set; }
    }
}
