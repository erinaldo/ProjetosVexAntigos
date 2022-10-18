using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDILayOut
    {
        public EDILayOut()
        {
            this.EDILayOutPerguntas = new List<EDILayOutPergunta>();
            this.EDILayOutRegs = new List<EDILayOutReg>();
            this.EDILayoutTabelas = new List<EDILayoutTabela>();
        }

        public int IDEDILayOut { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public string Versao { get; set; }
        public string Revisao { get; set; }
        public virtual ICollection<EDILayOutPergunta> EDILayOutPerguntas { get; set; }
        public virtual ICollection<EDILayOutReg> EDILayOutRegs { get; set; }
        public virtual ICollection<EDILayoutTabela> EDILayoutTabelas { get; set; }
    }
}
