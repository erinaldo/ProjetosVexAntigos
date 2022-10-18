using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CotacaoFornecedorCondPgto
    {
        public int IdCotacaoFornecedorCondPgto { get; set; }
        public int IdCotacaoFornecedor { get; set; }
        public System.DateTime Vencimento { get; set; }
        public decimal Valor { get; set; }
        public virtual CotacaoFornecedor CotacaoFornecedor { get; set; }
    }
}
