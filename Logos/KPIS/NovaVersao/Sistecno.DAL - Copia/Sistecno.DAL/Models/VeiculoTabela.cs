using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class VeiculoTabela
    {
        public VeiculoTabela()
        {
            this.VeiculoTabelaAgregadoes = new List<VeiculoTabelaAgregado>();
            this.VeiculoTabelaRegiaos = new List<VeiculoTabelaRegiao>();
        }

        public int IdVeiculoTabela { get; set; }
        public int IdVeiculoTipo { get; set; }
        public Nullable<int> IdVeiculo { get; set; }
        public Nullable<int> IdFilial { get; set; }
        public Nullable<decimal> Diaria { get; set; }
        public Nullable<decimal> Ajudante { get; set; }
        public Nullable<decimal> ValorPorEntrega { get; set; }
        public Nullable<decimal> ValorKM { get; set; }
        public Nullable<decimal> ValorKG { get; set; }
        public Nullable<decimal> PercentualProximaSaida { get; set; }
        public Nullable<System.DateTime> Cadastro { get; set; }
        public Nullable<System.DateTime> UltimaAlteracao { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Veiculo Veiculo { get; set; }
        public virtual VeiculoTipo VeiculoTipo { get; set; }
        public virtual ICollection<VeiculoTabelaAgregado> VeiculoTabelaAgregadoes { get; set; }
        public virtual ICollection<VeiculoTabelaRegiao> VeiculoTabelaRegiaos { get; set; }
    }
}
