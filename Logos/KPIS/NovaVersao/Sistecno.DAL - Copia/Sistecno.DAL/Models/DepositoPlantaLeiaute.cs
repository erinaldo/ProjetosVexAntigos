using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DepositoPlantaLeiaute
    {
        public int IDDepositoPlantaLeiaute { get; set; }
        public int IDDepositoPlanta { get; set; }
        public string Tipo { get; set; }
        public string TipoCliente { get; set; }
        public string Inicio { get; set; }
        public string Fim { get; set; }
        public Nullable<int> Quantidade { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public string Descricao { get; set; }
        public virtual DepositoPlanta DepositoPlanta { get; set; }
    }
}
