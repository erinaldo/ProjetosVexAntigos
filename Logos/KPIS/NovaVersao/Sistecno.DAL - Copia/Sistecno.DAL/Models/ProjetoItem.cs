using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ProjetoItem
    {
        public int IdProjetoItem { get; set; }
        public int IdProjeto { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public Nullable<decimal> Quantidade { get; set; }
        public Nullable<decimal> QuantidadeRecebida { get; set; }
        public Nullable<System.DateTime> UltimoRecebimento { get; set; }
        public virtual Projeto Projeto { get; set; }
    }
}
