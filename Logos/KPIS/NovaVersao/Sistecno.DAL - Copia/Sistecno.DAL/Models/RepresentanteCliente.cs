using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class RepresentanteCliente
    {
        public int IdRepresentanteCliente { get; set; }
        public int IdRepresentante { get; set; }
        public int IdCliente { get; set; }
        public Nullable<decimal> Comissao { get; set; }
        public string Tipo { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Representante Representante { get; set; }
    }
}
