using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ArquivoItem
    {
        public int IdArquivoItem { get; set; }
        public int IdArquivo { get; set; }
        public string NomeDoArquivo { get; set; }
        public byte[] ConteudoArquivo { get; set; }
        public virtual Arquivo Arquivo { get; set; }
    }
}
