using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Cheque
    {
        public Cheque()
        {
            this.DtCheques = new List<DtCheque>();
            this.MovimentacaoBancarias = new List<MovimentacaoBancaria>();
        }

        public int IDCheque { get; set; }
        public Nullable<int> IDBancoConta { get; set; }
        public int IDBanco { get; set; }
        public string Numero { get; set; }
        public string Serie { get; set; }
        public System.DateTime DataDeEmissao { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public string Situacao { get; set; }
        public string Portador { get; set; }
        public System.DateTime DataDeDisponibilidade { get; set; }
        public string PagarReceber { get; set; }
        public virtual Banco Banco { get; set; }
        public virtual BancoConta BancoConta { get; set; }
        public virtual ICollection<DtCheque> DtCheques { get; set; }
        public virtual ICollection<MovimentacaoBancaria> MovimentacaoBancarias { get; set; }
    }
}
