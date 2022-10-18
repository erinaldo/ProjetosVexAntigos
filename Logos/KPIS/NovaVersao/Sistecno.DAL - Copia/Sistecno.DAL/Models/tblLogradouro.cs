using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class tblLogradouro
    {
        public int Codigo { get; set; }
        public string UF { get; set; }
        public int CodigoCidade { get; set; }
        public string DescricaoNaoAbreviada { get; set; }
        public string Descricao { get; set; }
        public Nullable<int> CodigoBairro { get; set; }
        public string CEP { get; set; }
        public Nullable<int> BAI_NU_SEQUENCIAL_FIM { get; set; }
        public string LOG_COMPLEMENTO { get; set; }
        public string LOG_TIPO_LOGRADOURO { get; set; }
        public string LOG_STATUS_TIPO_LOG { get; set; }
        public string DescricaoSemAcento { get; set; }
    }
}
