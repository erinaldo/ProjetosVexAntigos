using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class FornecedorFilial
    {
        public int IDFornecedorFilial { get; set; }
        public int IDFornecedor { get; set; }
        public int IDFilial { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
    }
}
