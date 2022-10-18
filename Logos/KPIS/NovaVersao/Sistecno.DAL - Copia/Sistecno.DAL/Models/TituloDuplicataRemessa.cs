using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class TituloDuplicataRemessa
    {
        public int IdTituloDuplicataRemessa { get; set; }
        public int IdTituloDuplicata { get; set; }
        public int IDBancoOcorrenciaRemessa { get; set; }
        public Nullable<System.DateTime> DataMovimento { get; set; }
        public Nullable<System.DateTime> DataDeEnvio { get; set; }
        public Nullable<int> IdUsuarioQueEnviou { get; set; }
        public string Situacao { get; set; }
        public virtual BancoOcorrenciaRemessa BancoOcorrenciaRemessa { get; set; }
        public virtual TituloDuplicata TituloDuplicata { get; set; }
    }
}
