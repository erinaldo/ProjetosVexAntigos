using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ClienteSetorFilial
    {
        public int IdClienteSetorFilial { get; set; }
        public int IdCliente { get; set; }
        public Nullable<int> IdSetor { get; set; }
        public int IdFilial { get; set; }
        public string Roteiro { get; set; }
        public Nullable<int> NumeroDoRoteiro { get; set; }
        public Nullable<int> CodigoDoCliente { get; set; }
        public Nullable<int> CodigoDoClienteFilial { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Setor Setor { get; set; }
    }
}
