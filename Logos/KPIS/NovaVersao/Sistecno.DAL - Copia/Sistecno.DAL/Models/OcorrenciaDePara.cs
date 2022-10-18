using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class OcorrenciaDePara
    {
        public int IdOcorrenciaDePara { get; set; }
        public int IdOcorrencia { get; set; }
        public Nullable<int> CodigoDoCliente { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public virtual Ocorrencia Ocorrencia { get; set; }
    }
}
