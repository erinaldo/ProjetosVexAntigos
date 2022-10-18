using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDILayoutTabela
    {
        public int IDEDILayoutTabela { get; set; }
        public int IDEDILayout { get; set; }
        public string Tabela { get; set; }
        public Nullable<int> OrdemGravacao { get; set; }
        public string CampoUnico { get; set; }
        public virtual EDILayOut EDILayOut { get; set; }
    }
}
