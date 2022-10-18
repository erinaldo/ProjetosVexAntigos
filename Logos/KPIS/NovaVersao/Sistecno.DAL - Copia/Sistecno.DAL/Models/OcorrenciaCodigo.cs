using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class OcorrenciaCodigo
    {
        public int IDOcorrenciaCodigo { get; set; }
        public int IDOcorrencia { get; set; }
        public int IDOcorrenciaSerie { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public virtual Ocorrencia Ocorrencia { get; set; }
        public virtual OcorrenciaSerie OcorrenciaSerie { get; set; }
    }
}
