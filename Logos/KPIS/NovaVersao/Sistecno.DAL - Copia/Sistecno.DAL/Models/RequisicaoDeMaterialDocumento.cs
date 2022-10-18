using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class RequisicaoDeMaterialDocumento
    {
        public int IDRequisicaoDeMaterialDocumento { get; set; }
        public int IDRequisicaoDeMaterialItem { get; set; }
        public Nullable<int> IDDocumento { get; set; }
        public Nullable<int> IdDocumentoItem { get; set; }
        public Nullable<int> IdCotacaoDeCompraItem { get; set; }
        public Nullable<int> IdDocumentoNFSaida { get; set; }
    }
}
