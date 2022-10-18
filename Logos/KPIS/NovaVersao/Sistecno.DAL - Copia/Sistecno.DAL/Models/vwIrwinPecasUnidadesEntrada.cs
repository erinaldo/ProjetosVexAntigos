using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class vwIrwinPecasUnidadesEntrada
    {
        public Nullable<System.DateTime> DataDeEmissao { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public Nullable<int> IDGrupoDeProduto { get; set; }
        public int IDMovimentacaoItem { get; set; }
        public Nullable<decimal> QUANTIDADEUNIDADEESTOQUE { get; set; }
        public Nullable<decimal> FATOR { get; set; }
    }
}
