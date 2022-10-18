using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDILayoutRegTabela
    {
        public int IDEDILayoutRegTabela { get; set; }
        public int IDEDILayoutReg { get; set; }
        public string Tabela { get; set; }
        public string TabelaPrincipal { get; set; }
        public string CampoRelacionado { get; set; }
        public string Alias { get; set; }
        public string CampoUnico { get; set; }
        public string IncluirRegistro { get; set; }
        public string AlterarRegistro { get; set; }
        public string TabelaRelacionada { get; set; }
        public virtual EDILayOutReg EDILayOutReg { get; set; }
    }
}
