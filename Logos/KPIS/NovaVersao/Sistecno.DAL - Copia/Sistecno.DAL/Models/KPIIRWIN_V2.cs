using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class KPIIRWIN_V2
    {
        public int IdKPIIRWIN { get; set; }
        public string Chave { get; set; }
        public string Descricao { get; set; }
        public string DescricaoUnidadeDeMedida { get; set; }
        public string DescricaoTarguet { get; set; }
        public Nullable<decimal> Target { get; set; }
        public string UnidadeTarget { get; set; }
        public string Calculado { get; set; }
        public Nullable<decimal> Ordem { get; set; }
    }
}
