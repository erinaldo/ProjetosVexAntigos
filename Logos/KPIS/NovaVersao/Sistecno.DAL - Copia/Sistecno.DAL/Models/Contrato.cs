using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Contrato
    {
        public Contrato()
        {
            this.ContratoCapas = new List<ContratoCapa>();
            this.ContratoImagems = new List<ContratoImagem>();
            this.ContratoItems = new List<ContratoItem>();
        }

        public int IdContrato { get; set; }
        public int IdFilial { get; set; }
        public Nullable<int> IdFornecedor { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public Nullable<int> IdUsuarioCadastro { get; set; }
        public string ClienteFornecedor { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public string Numero { get; set; }
        public string Referencia { get; set; }
        public Nullable<int> Parcelas { get; set; }
        public Nullable<decimal> ValorTotal { get; set; }
        public Nullable<System.DateTime> PrimeiroVencimento { get; set; }
        public string Ativo { get; set; }
        public string ValorReal { get; set; }
        public virtual ICollection<ContratoCapa> ContratoCapas { get; set; }
        public virtual ICollection<ContratoImagem> ContratoImagems { get; set; }
        public virtual ICollection<ContratoItem> ContratoItems { get; set; }
    }
}
