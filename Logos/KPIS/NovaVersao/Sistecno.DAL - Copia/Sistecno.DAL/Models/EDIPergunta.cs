using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDIPergunta
    {
        public int IDEDIPergunta { get; set; }
        public int IDEDI { get; set; }
        public string Pergunta { get; set; }
        public string Tipo { get; set; }
        public string ItensDaLista { get; set; }
        public string PesquisaPadrao { get; set; }
        public string Tabela { get; set; }
        public string Campo { get; set; }
        public string UsaIntervalo { get; set; }
    }
}
