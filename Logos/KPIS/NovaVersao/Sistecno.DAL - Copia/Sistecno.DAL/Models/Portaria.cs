using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Portaria
    {
        public Portaria()
        {
            this.PortariaVisitantes = new List<PortariaVisitante>();
            this.Romaneios = new List<Romaneio>();
        }

        public int IDPortaria { get; set; }
        public int IDFilial { get; set; }
        public Nullable<int> IdDt { get; set; }
        public Nullable<int> IDTransportadora { get; set; }
        public Nullable<int> IDVeiculo { get; set; }
        public Nullable<int> IdAgendamento { get; set; }
        public string Placa { get; set; }
        public Nullable<System.DateTime> DataHoraDeEntrada { get; set; }
        public Nullable<System.DateTime> DataHoraDeSaida { get; set; }
        public string Observacao { get; set; }
        public string Situacao { get; set; }
        public string Transportadora { get; set; }
        public string Cliente { get; set; }
        public string Motorista { get; set; }
        public string RGMotorista { get; set; }
        public string Ajudante { get; set; }
        public string RGAjudante { get; set; }
        public string Nome { get; set; }
        public virtual Agendamento Agendamento { get; set; }
        public virtual DT DT { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Veiculo Veiculo { get; set; }
        public virtual ICollection<PortariaVisitante> PortariaVisitantes { get; set; }
        public virtual ICollection<Romaneio> Romaneios { get; set; }
    }
}
