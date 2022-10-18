using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class RetornoComprovei
    {
        public int IdRetornoComprovei { get; set; }
        public int IdDocumento { get; set; }
        public string Chave { get; set; }
        public System.DateTime DataDaOcorrencia { get; set; }
        public string Ocorrencia { get; set; }
        public int IdOcorrenciaComprovei { get; set; }
        public byte[] Foto { get; set; }
        public Nullable<int> IdDocumentoOcorrencia { get; set; }
        public Nullable<System.DateTime> Processado { get; set; }
        public Nullable<System.DateTime> HorarioRecebimento { get; set; }
        public Nullable<System.DateTime> HorarioProcessamento { get; set; }
    }
}
