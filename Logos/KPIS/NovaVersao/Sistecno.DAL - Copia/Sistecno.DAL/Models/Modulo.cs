using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Modulo
    {
        public int IDModulo { get; set; }
        public string Nome { get; set; }
        public int Ordem { get; set; }
        public string Tipo { get; set; }
    }
}
