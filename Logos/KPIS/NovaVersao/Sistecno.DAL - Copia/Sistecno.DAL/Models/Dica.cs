using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Dica
    {
        public int IDDica { get; set; }
        public string Titulo { get; set; }
        public Nullable<System.DateTime> DicaDoDia { get; set; }
        public string Texto { get; set; }
    }
}
