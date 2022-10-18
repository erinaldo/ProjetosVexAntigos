using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UnidadeDeMedida
    {
        public UnidadeDeMedida()
        {
            this.ProdutoClientes = new List<ProdutoCliente>();
        }

        public int IDUnidadeDeMedida { get; set; }
        public string Unidade { get; set; }
        public string Nome { get; set; }
        public Nullable<int> Decimais { get; set; }
        public virtual ICollection<ProdutoCliente> ProdutoClientes { get; set; }
    }
}
