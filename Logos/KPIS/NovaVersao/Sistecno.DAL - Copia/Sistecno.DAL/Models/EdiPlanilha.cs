using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EdiPlanilha
    {
        public int IdEdiPlanilha { get; set; }
        public string Arquivo { get; set; }
        public Nullable<System.DateTime> DataHora { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaAtualizacao { get; set; }
    }
}
