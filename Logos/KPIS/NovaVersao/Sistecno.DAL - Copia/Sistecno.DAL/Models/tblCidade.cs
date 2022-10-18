using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class tblCidade
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string Descricao_B { get; set; }
        public string CEP { get; set; }
        public string UF { get; set; }
        public int SITUACAO { get; set; }
        public string TIPO_LOCALIDADE { get; set; }
        public Nullable<int> LOC_NU_SEQUENCIAL_SUB { get; set; }
    }
}
