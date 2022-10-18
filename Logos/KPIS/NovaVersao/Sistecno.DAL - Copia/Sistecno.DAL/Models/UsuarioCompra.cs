using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UsuarioCompra
    {
        public int IdUsuarioCompra { get; set; }
        public int IdUsuario { get; set; }
        public Nullable<int> IdUnidadeFuncional { get; set; }
        public Nullable<decimal> LimiteDeCompra { get; set; }
        public string Ativo { get; set; }
        public Nullable<decimal> LimiteDeCompraMinimo { get; set; }
        public virtual UnidadeFuncional UnidadeFuncional { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
