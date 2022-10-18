using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ProjetoProducao
    {
        public int IdProjetoProducao { get; set; }
        public int IdProjeto { get; set; }
        public System.DateTime Lancamento { get; set; }
        public string Turno { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> HoraInicial { get; set; }
        public Nullable<System.DateTime> HoraFinal { get; set; }
        public Nullable<int> MaoDeObra { get; set; }
        public Nullable<int> QuantidadeEfetuada { get; set; }
        public virtual Projeto Projeto { get; set; }
    }
}
