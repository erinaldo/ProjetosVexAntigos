using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class AgendaRecebimento
    {
        public AgendaRecebimento()
        {
            this.Agendamentoes = new List<Agendamento>();
        }

        public int IdAgendaRecebimento { get; set; }
        public int IdFilial { get; set; }
        public int IdDepositoPlantaLocalizacao { get; set; }
        public Nullable<int> IdAgendamento { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public string Hora { get; set; }
        public string Disponivel { get; set; }
        public virtual ICollection<Agendamento> Agendamentoes { get; set; }
        public virtual Agendamento Agendamento { get; set; }
        public virtual DepositoPlantaLocalizacao DepositoPlantaLocalizacao { get; set; }
        public virtual Filial Filial { get; set; }
    }
}
