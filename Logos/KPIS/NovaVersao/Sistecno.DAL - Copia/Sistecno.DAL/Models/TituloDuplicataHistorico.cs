using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class TituloDuplicataHistorico
    {
        public int IDTituloDuplicataHistorico { get; set; }
        public int IDTituloDuplicata { get; set; }
        public string Historico { get; set; }
        public System.DateTime DataDeCadastro { get; set; }
        public int IDUsuario { get; set; }
        public Nullable<int> NumeroDoArquivo { get; set; }
        public virtual TituloDuplicata TituloDuplicata { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
