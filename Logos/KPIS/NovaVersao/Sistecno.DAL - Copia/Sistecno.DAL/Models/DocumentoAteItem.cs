using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoAteItem
    {
        public int IDDocumentoAteItem { get; set; }
        public int IDDocumentoAte { get; set; }
        public string TipoDocumento { get; set; }
        public Nullable<int> IDCidade { get; set; }
        public Nullable<int> IDEstado { get; set; }
        public Nullable<decimal> ValorDoc { get; set; }
        public string NumeroDoc { get; set; }
        public Nullable<int> IDCidadeColeta { get; set; }
        public Nullable<int> IDEstadoColeta { get; set; }
    }
}
