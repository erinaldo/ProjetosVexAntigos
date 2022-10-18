using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ContaDespesa
    {
        public ContaDespesa()
        {
            this.ContaDespesaCapas = new List<ContaDespesaCapa>();
            this.ContaDespesaImagems = new List<ContaDespesaImagem>();
            this.ContaDespesaItems = new List<ContaDespesaItem>();
            this.ContaDespesaObservacaos = new List<ContaDespesaObservacao>();
        }

        public int IdContaDespesa { get; set; }
        public string TipoDeDespesa { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public Nullable<int> Numero { get; set; }
        public string Referencia { get; set; }
        public Nullable<int> Parcelas { get; set; }
        public Nullable<decimal> ValorTotal { get; set; }
        public Nullable<System.DateTime> PrimeiroVencimento { get; set; }
        public Nullable<int> IdFilial { get; set; }
        public string Nome { get; set; }
        public Nullable<int> IdFornecedor { get; set; }
        public virtual ICollection<ContaDespesaCapa> ContaDespesaCapas { get; set; }
        public virtual ICollection<ContaDespesaImagem> ContaDespesaImagems { get; set; }
        public virtual ICollection<ContaDespesaItem> ContaDespesaItems { get; set; }
        public virtual ICollection<ContaDespesaObservacao> ContaDespesaObservacaos { get; set; }
    }
}
