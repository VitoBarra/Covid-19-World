using System;
using System.Collections.Generic;

namespace Covid_World.ModelsDB
{
    public partial class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
    }
}
