using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ClienteTipoDeMaterial
    {
        public ClienteTipoDeMaterial()
        {
            this.ProdutoClientes = new List<ProdutoCliente>();
        }

        public int IDClienteTipoDeMaterial { get; set; }
        public int IDCliente { get; set; }
        public string Nome { get; set; }
        public string Solicitar { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<ProdutoCliente> ProdutoClientes { get; set; }
    }
}
