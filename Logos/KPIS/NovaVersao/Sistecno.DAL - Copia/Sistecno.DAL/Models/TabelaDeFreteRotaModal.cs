using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class TabelaDeFreteRotaModal
    {
        public TabelaDeFreteRotaModal()
        {
            this.TabelaDeFreteRotaModalValors = new List<TabelaDeFreteRotaModalValor>();
        }

        public int IdTabelaDeFreteRotaModal { get; set; }
        public int IdTabelaDeFreteRota { get; set; }
        public int IdModal { get; set; }
        public Nullable<int> PrazoDeEntrega { get; set; }
        public Nullable<decimal> FatorDeCubagem { get; set; }
        public virtual Modal Modal { get; set; }
        public virtual TabelaDeFreteRota TabelaDeFreteRota { get; set; }
        public virtual ICollection<TabelaDeFreteRotaModalValor> TabelaDeFreteRotaModalValors { get; set; }
    }
}
