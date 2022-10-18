using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoCotacao
    {
        public int IddocumentoCotacao { get; set; }
        public Nullable<int> IdDocumento { get; set; }
        public Nullable<int> IdCotacaoFornecedor { get; set; }
        public Nullable<int> IdCotacaoDeCompra { get; set; }
    }
}
