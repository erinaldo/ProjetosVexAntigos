using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDILayOutReg
    {
        public EDILayOutReg()
        {
            this.EDILayOutReg1 = new List<EDILayOutReg>();
            this.EDILayOutRegCpoes = new List<EDILayOutRegCpo>();
            this.EDILayoutRegTabelas = new List<EDILayoutRegTabela>();
        }

        public int IDEDILayOutReg { get; set; }
        public int IDEDILayOut { get; set; }
        public string Descricao { get; set; }
        public int Ordem { get; set; }
        public Nullable<int> Pai { get; set; }
        public string Condicao { get; set; }
        public string TabelaChave { get; set; }
        public string CampoChave { get; set; }
        public string TabelaPai { get; set; }
        public string CampoPai { get; set; }
        public string Obrigatorio { get; set; }
        public virtual EDILayOut EDILayOut { get; set; }
        public virtual ICollection<EDILayOutReg> EDILayOutReg1 { get; set; }
        public virtual EDILayOutReg EDILayOutReg2 { get; set; }
        public virtual ICollection<EDILayOutRegCpo> EDILayOutRegCpoes { get; set; }
        public virtual ICollection<EDILayoutRegTabela> EDILayoutRegTabelas { get; set; }
    }
}
