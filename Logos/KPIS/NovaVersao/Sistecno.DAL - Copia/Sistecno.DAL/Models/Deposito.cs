using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Deposito
    {
        public Deposito()
        {
            this.DepositoPlantas = new List<DepositoPlanta>();
        }

        public int IDDeposito { get; set; }
        public int IDFilial { get; set; }
        public string Descricao { get; set; }
        public string Endereco { get; set; }
        public Nullable<decimal> AreaTotal { get; set; }
        public Nullable<decimal> AreaUtil { get; set; }
        public Nullable<decimal> Largura { get; set; }
        public Nullable<decimal> Profundidade { get; set; }
        public Nullable<decimal> Altura { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public string Ativo { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual ICollection<DepositoPlanta> DepositoPlantas { get; set; }
    }
}
