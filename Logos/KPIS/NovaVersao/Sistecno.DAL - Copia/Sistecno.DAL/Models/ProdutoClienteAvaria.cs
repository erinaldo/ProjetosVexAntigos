using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ProdutoClienteAvaria
    {
        public int IdProdutoClienteAvaria { get; set; }
        public int IdProdutoCliente { get; set; }
        public Nullable<decimal> Unidades { get; set; }
        public System.DateTime Data { get; set; }
        public string TipoDeAvaria { get; set; }
        public virtual ProdutoCliente ProdutoCliente { get; set; }
    }
}
