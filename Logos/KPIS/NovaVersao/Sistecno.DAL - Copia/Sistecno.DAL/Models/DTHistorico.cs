using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DTHistorico
    {
        public int IdDTHistorico { get; set; }
        public int IdDT { get; set; }
        public string Historico { get; set; }
        public System.DateTime DataDeCadastro { get; set; }
        public int IDUsuario { get; set; }
        public Nullable<System.DateTime> DataDaOcorrencia { get; set; }
        public virtual DT DT { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
