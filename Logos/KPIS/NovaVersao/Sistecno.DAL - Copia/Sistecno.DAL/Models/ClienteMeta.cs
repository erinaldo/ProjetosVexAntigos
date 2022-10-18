using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ClienteMeta
    {
        public int IdClienteMeta { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public int Prazo { get; set; }
        public string Cor { get; set; }
        public decimal Percentual { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
