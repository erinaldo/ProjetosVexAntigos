using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ProjetoFilial
    {
        public int IdProjetoFilial { get; set; }
        public int IdProjeto { get; set; }
        public int IdFilial { get; set; }
        public string Tipo { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Projeto Projeto { get; set; }
    }
}
