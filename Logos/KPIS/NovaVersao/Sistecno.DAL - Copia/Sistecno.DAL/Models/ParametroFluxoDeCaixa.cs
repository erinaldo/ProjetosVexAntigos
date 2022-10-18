using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ParametroFluxoDeCaixa
    {
        public int IdParametroFluxoDeCaixa { get; set; }
        public Nullable<int> IdEmpresa { get; set; }
        public string TituloContaPagar { get; set; }
        public string ConsideraSaldoAnterior { get; set; }
        public Nullable<int> DiasAnteriores { get; set; }
        public Nullable<int> DiasPosteriores { get; set; }
        public string Ativo { get; set; }
        public Nullable<System.DateTime> DataInicial { get; set; }
        public Nullable<System.DateTime> DataFinal { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public string HabilitarCompensacao { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}
