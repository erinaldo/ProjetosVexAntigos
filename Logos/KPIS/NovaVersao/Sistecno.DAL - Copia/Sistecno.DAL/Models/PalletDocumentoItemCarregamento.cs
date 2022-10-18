using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class PalletDocumentoItemCarregamento
    {
        public int IdPalletDocumentoItemCarregamento { get; set; }
        public int IdPalletDocumento { get; set; }
        public Nullable<int> IdDocumentoItem { get; set; }
        public Nullable<int> IdProduto { get; set; }
        public Nullable<decimal> Quantidade { get; set; }
    }
}
