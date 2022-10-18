using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class VeiculoTabelaAgregado
    {
        public int IdVeiculoTabelaAgregado { get; set; }
        public int IdVeiculoTabela { get; set; }
        public int IdVeiculo { get; set; }
        public virtual Veiculo Veiculo { get; set; }
        public virtual VeiculoTabela VeiculoTabela { get; set; }
    }
}
