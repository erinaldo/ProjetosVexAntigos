using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Orcamento
    {
        public Orcamento()
        {
            this.OrcamentoPedidoes = new List<OrcamentoPedido>();
        }

        public int IdOrcamento { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public string Orcamento1 { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public string Solicitante { get; set; }
        public string Email { get; set; }
        public string Fone { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public string Status { get; set; }
        public virtual ICollection<OrcamentoPedido> OrcamentoPedidoes { get; set; }
    }
}
