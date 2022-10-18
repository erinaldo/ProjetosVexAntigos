using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ColetorConferenciaLog
    {
        public int IdColetorConferenciaLog { get; set; }
        public int IdFilial { get; set; }
        public int IdUsuario { get; set; }
        public int IdDocumento { get; set; }
        public System.DateTime Data { get; set; }
        public string Status { get; set; }
        public Nullable<int> VolumesFaltantes { get; set; }
        public string CodigoRetorno { get; set; }
        public string DescricaoRetorno { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
