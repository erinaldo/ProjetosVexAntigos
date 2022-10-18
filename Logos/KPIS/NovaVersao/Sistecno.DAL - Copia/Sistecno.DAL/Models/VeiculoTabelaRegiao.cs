using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class VeiculoTabelaRegiao
    {
        public VeiculoTabelaRegiao()
        {
            this.VeiculoTabelaRegiaoItems = new List<VeiculoTabelaRegiaoItem>();
        }

        public int IdVeiculoTabelaRegiao { get; set; }
        public int IdVeiculoTabela { get; set; }
        public int IdRegiao { get; set; }
        public Nullable<System.DateTime> Cadastro { get; set; }
        public virtual Regiao Regiao { get; set; }
        public virtual VeiculoTabela VeiculoTabela { get; set; }
        public virtual ICollection<VeiculoTabelaRegiaoItem> VeiculoTabelaRegiaoItems { get; set; }
    }
}
