using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class OrcamentoPedido
    {
        public OrcamentoPedido()
        {
            this.OrcamentoPedidoItems = new List<OrcamentoPedidoItem>();
        }

        public int IdOrcamentoPedido { get; set; }
        public int IdOrcamento { get; set; }
        public int IdCidade { get; set; }
        public Nullable<int> IdModal { get; set; }
        public Nullable<int> IdPlanilha { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<decimal> Quantidade { get; set; }
        public Nullable<decimal> PesoLiquido { get; set; }
        public Nullable<decimal> PesoBruto { get; set; }
        public Nullable<decimal> PesoCubado { get; set; }
        public Nullable<decimal> MetragemCubica { get; set; }
        public Nullable<decimal> Frete { get; set; }
        public Nullable<decimal> BaseDeCalculo { get; set; }
        public Nullable<decimal> Aliquota { get; set; }
        public Nullable<decimal> Icms { get; set; }
        public Nullable<decimal> FreteTotal { get; set; }
        public virtual Orcamento Orcamento { get; set; }
        public virtual ICollection<OrcamentoPedidoItem> OrcamentoPedidoItems { get; set; }
    }
}
