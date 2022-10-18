using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class PalletDocumentoItem
    {
        public int IdPalletDocumentoItem { get; set; }
        public int IdPalletDocumento { get; set; }
        public Nullable<int> IdDocumentoItem { get; set; }
        public Nullable<int> IdProduto { get; set; }
        public Nullable<decimal> Quantidade { get; set; }
        public Nullable<int> idProdutoEmbalagemPallet { get; set; }
        public virtual DocumentoItem DocumentoItem { get; set; }
        public virtual PalletDocumento PalletDocumento { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
