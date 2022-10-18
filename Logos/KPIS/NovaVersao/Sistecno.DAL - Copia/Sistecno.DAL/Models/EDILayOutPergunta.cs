using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDILayOutPergunta
    {
        public int IDEDILayoutPergunta { get; set; }
        public int IDEDILayout { get; set; }
        public string Pergunta { get; set; }
        public string Tipo { get; set; }
        public string ItensDaLista { get; set; }
        public string PesquisaPadrao { get; set; }
        public string Tabela { get; set; }
        public string Campo { get; set; }
        public string UsaIntervalo { get; set; }
        public virtual EDILayOut EDILayOut { get; set; }
    }
}
