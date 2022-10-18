using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class vwIrwinPecasUnidadesRessuprimento
    {
        public Nullable<System.DateTime> DATADECADASTRO { get; set; }
        public Nullable<int> IDGRUPODEPRODUTO { get; set; }
        public int IDMOVIMENTACAOITEM { get; set; }
        public Nullable<decimal> QUANTIDADEUNIDADEESTOQUE { get; set; }
        public Nullable<decimal> FATOR { get; set; }
    }
}
