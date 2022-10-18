using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DepositoPlanta
    {
        public DepositoPlanta()
        {
            this.DepositoPlantaLeiautes = new List<DepositoPlantaLeiaute>();
            this.DepositoPlantaLocalizacaos = new List<DepositoPlantaLocalizacao>();
        }

        public int IDDepositoPlanta { get; set; }
        public int IDDeposito { get; set; }
        public string Descricao { get; set; }
        public Nullable<decimal> AreaTotal { get; set; }
        public Nullable<decimal> AreaUtil { get; set; }
        public Nullable<decimal> Largura { get; set; }
        public Nullable<decimal> Profundidade { get; set; }
        public Nullable<decimal> Altura { get; set; }
        public Nullable<decimal> LocalLarguraPadrao { get; set; }
        public Nullable<decimal> LocalProfundidadePadrao { get; set; }
        public Nullable<decimal> LocalAlturaPadrao { get; set; }
        public Nullable<decimal> LocalCapacidadePadrao { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public string Ativo { get; set; }
        public virtual Deposito Deposito { get; set; }
        public virtual ICollection<DepositoPlantaLeiaute> DepositoPlantaLeiautes { get; set; }
        public virtual ICollection<DepositoPlantaLocalizacao> DepositoPlantaLocalizacaos { get; set; }
    }
}
