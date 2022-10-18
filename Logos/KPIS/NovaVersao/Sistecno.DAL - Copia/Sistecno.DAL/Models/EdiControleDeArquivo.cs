using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EdiControleDeArquivo
    {
        public int IdEDIControleDeArquivo { get; set; }
        public int IdUsuario { get; set; }
        public string ChaveDoArquivo { get; set; }
        public string NomeDoMetodo { get; set; }
        public string NomeDoArquivo { get; set; }
        public string EnderecoDoArquivo { get; set; }
        public Nullable<System.DateTime> DataHoraInicio { get; set; }
        public Nullable<System.DateTime> DataHoraFinal { get; set; }
        public string TipoDeArquivo { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
