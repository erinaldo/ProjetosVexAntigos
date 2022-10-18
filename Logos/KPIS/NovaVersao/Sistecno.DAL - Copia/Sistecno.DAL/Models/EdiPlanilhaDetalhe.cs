using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EdiPlanilhaDetalhe
    {
        public int IdEdiPlanilhaDetalhe { get; set; }
        public Nullable<int> IdEdiPlanilha { get; set; }
        public Nullable<int> SequenciaPlanilha { get; set; }
        public string CnpjCpf { get; set; }
        public Nullable<int> IdDocumento { get; set; }
        public string Rejeicao { get; set; }
    }
}
