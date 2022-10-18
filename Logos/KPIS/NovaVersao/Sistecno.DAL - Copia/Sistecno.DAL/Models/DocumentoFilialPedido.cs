using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoFilialPedido
    {
        public int IDDocumentoFilial { get; set; }
        public int IDDocumento { get; set; }
        public int IDFilial { get; set; }
        public int IDRegiaoItem { get; set; }
        public Nullable<int> IdRegiaoItemFilial { get; set; }
        public Nullable<int> IdRegiaoItemCliente { get; set; }
        public Nullable<int> IdRegiaoItemTransportador { get; set; }
        public string Situacao { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public Nullable<int> IdSetor { get; set; }
    }
}
