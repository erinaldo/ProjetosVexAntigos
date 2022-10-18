using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ReposicaoRogeItem
    {
        public ReposicaoRogeItem()
        {
            this.ReposicaoRogeCBs = new List<ReposicaoRogeCB>();
        }

        public int IdReposicaoRogeItem { get; set; }
        public Nullable<int> IdReposicaoRoge { get; set; }
        public string CodigoRoge { get; set; }
        public Nullable<System.DateTime> DataDaInclusao { get; set; }
        public Nullable<System.DateTime> DataConferido { get; set; }
        public Nullable<int> QuantidadeLido { get; set; }
        public string PerteceANota { get; set; }
        public string CodigoBarrasLido { get; set; }
        public string Descricao { get; set; }
        public Nullable<int> QuantidadeNota { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<decimal> ValorTotal { get; set; }
        public string Pago { get; set; }
        public string ObservacaoPago { get; set; }
        public virtual ReposicaoRoge ReposicaoRoge { get; set; }
        public virtual ICollection<ReposicaoRogeCB> ReposicaoRogeCBs { get; set; }
    }
}
