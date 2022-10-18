using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class TB_AUX_RE
    {
        public string FILIAL { get; set; }
        public Nullable<decimal> IMPOSTO { get; set; }
        public Nullable<decimal> TAXAADMINISTRATIVA { get; set; }
        public Nullable<decimal> SEGURO { get; set; }
        public Nullable<decimal> TAXADETRANFERENCIA { get; set; }
        public int NUMERO { get; set; }
        public Nullable<System.DateTime> EMISSAO { get; set; }
        public string MOTORISTA { get; set; }
        public Nullable<int> ENTREGAS { get; set; }
        public Nullable<decimal> PESO { get; set; }
        public Nullable<decimal> VALOR_DA_NOTA { get; set; }
        public Nullable<decimal> VALOR_DO_FRETE { get; set; }
        public Nullable<decimal> PERC_FRETE { get; set; }
        public Nullable<decimal> FRETE_MOTORISTA { get; set; }
        public Nullable<decimal> VLIMPOSTOS { get; set; }
        public Nullable<decimal> VLSEGURO { get; set; }
        public Nullable<decimal> ADM { get; set; }
        public Nullable<decimal> TRANSF { get; set; }
        public Nullable<decimal> LUCRO { get; set; }
        public Nullable<decimal> PER_LUCRO { get; set; }
    }
}
