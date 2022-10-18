using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoOrcamento
    {
        public int IDDocumentoOrcamento { get; set; }
        public int IDDocumento { get; set; }
        public Nullable<int> IDUsuario { get; set; }
        public Nullable<System.DateTime> DataDoMovimento { get; set; }
        public string Descricao { get; set; }
        public string Nome { get; set; }
        public string Extencao { get; set; }
        public byte[] Arquivo { get; set; }
        public string Status { get; set; }
        public string Motivo { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
