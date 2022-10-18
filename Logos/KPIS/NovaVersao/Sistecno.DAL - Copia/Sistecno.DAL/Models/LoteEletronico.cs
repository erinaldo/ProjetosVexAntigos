using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class LoteEletronico
    {
        public int IdLoteEletronico { get; set; }
        public string Descricao { get; set; }
        public string Recibo { get; set; }
        public string CStatus { get; set; }
        public string Status { get; set; }
        public string Xml { get; set; }
        public Nullable<System.DateTime> LoteGerado { get; set; }
        public Nullable<System.DateTime> LoteEnviadoAoSefaz { get; set; }
        public Nullable<System.DateTime> ConsultaDoRecibo { get; set; }
        public Nullable<int> IdFilial { get; set; }
        public string NomeDaMaquina { get; set; }
        public string EnvLot { get; set; }
        public string EnvLotXML { get; set; }
    }
}
