using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class VeiculoTabelaRegiaoItem
    {
        public int IdVeiculoTabelaRegiaoItem { get; set; }
        public int IdVeiculoTabelaRegiao { get; set; }
        public int IdRegiaoItem { get; set; }
        public Nullable<decimal> KM { get; set; }
        public Nullable<decimal> ValorPorEntrega { get; set; }
        public Nullable<decimal> Adicional { get; set; }
        public Nullable<decimal> Pedagio { get; set; }
        public Nullable<System.DateTime> Cadastro { get; set; }
        public Nullable<System.DateTime> UltimaAlteracao { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public virtual VeiculoTabelaRegiao VeiculoTabelaRegiao { get; set; }
    }
}
